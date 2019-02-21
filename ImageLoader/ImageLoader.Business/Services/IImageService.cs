using ImageLoader.Business.Models;
using System.Collections.Generic;

namespace ImageLoader.Business.Services
{
    public interface IImageService
    {
        void Save(ImageInfoModel imageInfoModel);

        void SaveRange(IReadOnlyCollection<ImageInfoModel> imageInfoModels);

        IReadOnlyCollection<ImageInfoModel> GetAll();
    }
}
