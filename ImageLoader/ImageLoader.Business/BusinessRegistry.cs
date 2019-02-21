using ImageLoader.Business.Providers;
using ImageLoader.Business.Providers.Implementations;
using ImageLoader.Business.Services;
using ImageLoader.Business.Services.Implementations;
using StructureMap;

namespace ImageLoader.Business
{
    public class BusinessRegistry : Registry
    {
        public BusinessRegistry()
        {
            // services
            For<IImageService>().Use<ImageService>();

            // providers
            For<IImageProvider>().Use<ImageProvider>();
            For<IImageSourceProvider>().Use<ImageSourceProvider>();
        }
    }
}
