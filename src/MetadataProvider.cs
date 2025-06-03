using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Jellyfin.Plugin.AudiobookMetadata.Models;
using System.Text.Json;

namespace Jellyfin.Plugin.AudiobookMetadata
{
    public class MetadataProvider
    {
        private readonly HttpClient _httpClient;

        public MetadataProvider()
        {
            _httpClient = new HttpClient();
        }

        public async Task<AudiobookMetadata> GetMetadataAsync(string identifier)
        {
            // Try Google Books API first
            var googleBooksUrl = $"https://www.googleapis.com/books/v1/volumes?q=isbn:{identifier}";
            var googleResponse = await _httpClient.GetStringAsync(googleBooksUrl);
            var metadata = ParseGoogleBooksMetadata(googleResponse);

            if (metadata != null)
            {
                return metadata;
            }

            // Fallback to Open Library API
            var openLibraryUrl = $"https://openlibrary.org/api/books?bibkeys=ISBN:{identifier}&format=json&jscmd=data";
            var openLibraryResponse = await _httpClient.GetStringAsync(openLibraryUrl);
            metadata = ParseOpenLibraryMetadata(openLibraryResponse, identifier);

            return metadata ?? new AudiobookMetadata
            {
                Title = "Unknown",
                Author = "Unknown",
                Narrator = "Unknown",
                Duration = TimeSpan.Zero
            };
        }

        private AudiobookMetadata ParseGoogleBooksMetadata(string jsonResponse)
        {
            using var doc = JsonDocument.Parse(jsonResponse);
            var root = doc.RootElement;

            if (root.TryGetProperty("items", out var items) && items.GetArrayLength() > 0)
            {
                var volumeInfo = items[0].GetProperty("volumeInfo");
                return new AudiobookMetadata
                {
                    Title = volumeInfo.GetProperty("title").GetString(),
                    Author = volumeInfo.TryGetProperty("authors", out var authors) && authors.GetArrayLength() > 0
                        ? authors[0].GetString()
                        : "Unknown",
                    Narrator = "Unknown", // Google Books API does not provide narrator info
                    Duration = TimeSpan.Zero // Not available
                };
            }
            return null;
        }

        private AudiobookMetadata ParseOpenLibraryMetadata(string jsonResponse, string identifier)
        {
            using var doc = JsonDocument.Parse(jsonResponse);
            var key = $"ISBN:{identifier}";
            if (doc.RootElement.TryGetProperty(key, out var book))
            {
                return new AudiobookMetadata
                {
                    Title = book.TryGetProperty("title", out var title) ? title.GetString() : "Unknown",
                    Author = book.TryGetProperty("authors", out var authors) && authors.GetArrayLength() > 0
                        ? authors[0].GetProperty("name").GetString()
                        : "Unknown",
                    Narrator = "Unknown", // Not available
                    Duration = TimeSpan.Zero // Not available
                };
            }
            return null;
        }
    }
}
