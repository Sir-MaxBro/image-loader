using ImageLoader.Business.Models;
using System.Collections.Generic;
using System.Linq;

namespace ImageLoader.Business.Services.Implementations
{
    internal class ImageService : IImageService
    {
        private static readonly object _saveSyncObject = new object();
        private static readonly List<ImageInfoModel> _images = new List<ImageInfoModel>();

        public IReadOnlyCollection<ImageInfoModel> GetAll()
        {
            return _images.ToList();
        }

        public void Save(ImageInfoModel imageInfoModel)
        {
            lock (_saveSyncObject)
            {
                _images.Add(imageInfoModel);
            }
        }

        public void SaveRange(IReadOnlyCollection<ImageInfoModel> imageInfoModels)
        {
            lock (_saveSyncObject)
            {
                _images.AddRange(imageInfoModels);
            }
        }
    }
}
