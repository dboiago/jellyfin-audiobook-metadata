using System;
using Jellyfin.Plugin;

namespace JellyfinAudiobookMetadataPlugin
{
    public class Plugin : IPlugin
    {
        public string Name => "Audiobook Metadata Plugin";
        public string Description => "A plugin to search for and add metadata for audiobooks.";
        public string Version => "1.0.0";

        public void Init()
        {
            // Register the metadata provider
            MetadataProvider provider = new MetadataProvider();
            PluginManager.Instance.RegisterMetadataProvider(provider);
        }
    }
}
