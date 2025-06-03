# Jellyfin Audiobook Metadata Plugin

This plugin enhances the Jellyfin media server by providing metadata for audiobooks. It allows users to search for and add detailed information about their audiobook collections, improving the overall experience of managing and enjoying audiobooks.

## Features

- Fetches metadata for audiobooks including title, author, narrator, and duration.
- Supports localization for easy translation.
- Integrates seamlessly with the Jellyfin media server.

## Installation

1. Clone the repository:
   ```
   git clone https://github.com/yourusername/jellyfin-audiobook-metadata-plugin.git
   ```

2. Navigate to the project directory:
   ```
   cd jellyfin-audiobook-metadata-plugin
   ```

3. Build the project using the .NET CLI:
   ```
   dotnet build
   ```

4. Copy the generated plugin DLL to the Jellyfin plugins directory:
   ```
   cp bin/Debug/netstandard2.0/Jellyfin.Plugin.AudiobookMetadata.dll /path/to/jellyfin/plugins/
   ```

5. Restart the Jellyfin server to load the new plugin.

## Usage

Once installed, the plugin will automatically search for audiobook metadata when audiobooks are added to your library. You can manage your audiobook collection through the Jellyfin web interface.

## Contributing

Contributions are welcome! Please fork the repository and submit a pull request with your changes. Ensure that your code adheres to the project's coding standards and includes appropriate tests.

## License

This project is licensed under the MIT License. See the LICENSE file for more details.
