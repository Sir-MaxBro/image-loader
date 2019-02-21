using System.Collections.Generic;

namespace ImageLoader.Business.Providers
{
    public interface IImageSourceProvider
    {
        IReadOnlyCollection<string> GetAllImageUrls();

        IReadOnlyCollection<string> GetRandomImageUrls(int imageCount);
    }
}
