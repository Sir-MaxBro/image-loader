using System;

namespace ImageLoader.Shared.Configurations.Implementations
{
    internal class ConfigurationProvider : IConfigurationProvider
    {
        public int GetSystemCoreCount() => Environment.ProcessorCount;
    }
}
