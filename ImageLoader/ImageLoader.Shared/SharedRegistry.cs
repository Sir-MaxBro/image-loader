using ImageLoader.Shared.Configurations;
using ImageLoader.Shared.Configurations.Implementations;
using StructureMap;

namespace ImageLoader.Shared
{
    public class SharedRegistry : Registry
    {
        public SharedRegistry()
        {
            // providers
            For<IConfigurationProvider>().Use<ConfigurationProvider>();
        }
    }
}
