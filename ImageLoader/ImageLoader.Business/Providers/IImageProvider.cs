using ImageLoader.Business.Constants;
using ImageLoader.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImageLoader.Business.Providers
{
    public interface IImageProvider
    {
        Task<IReadOnlyCollection<ImageInfoModel>> LoadImagesAsync(int imageCount = ImagesConstants.DefaultImageCount);
    }
}
