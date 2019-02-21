using ImageLoader.Shared.Configurations;
using ImageLoader.Shared.Configurations.Implementations;
using ImageLoader.Shared.Services;
using ImageLoader.Shared.Services.Implementations;
using StructureMap;

namespace ImageLoader.Shared
{
    public class SharedRegistry : Registry
    {
        public SharedRegistry()
        {
            // providers
            For<IConfigurationProvider>().Use<ConfigurationProvider>();

            // services
            For<ILoggerService>().Use<LoggerService>();
        }
    }
}
