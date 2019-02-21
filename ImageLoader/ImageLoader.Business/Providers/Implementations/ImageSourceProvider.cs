using System;
using System.Collections.Generic;
using System.Linq;

namespace ImageLoader.Business.Providers.Implementations
{
    internal class ImageSourceProvider : IImageSourceProvider
    {
        public IReadOnlyCollection<string> GetAllImageUrls()
        {
            var imageUrls = new List<string>();
            for (var i = 0; i < 10000; i++)
            {
                imageUrls.Add($"https://img.tyt.by/i/by5/weather/d/{i + 1}.png");
            }

            return imageUrls;
        }

        public IReadOnlyCollection<string> GetRandomImageUrls(int imageCount)
        {
            var allImageUrls = this.GetAllImageUrls();
            var randomGenerator = new Random(DateTime.UtcNow.Millisecond);
            var allImageUrlCount = allImageUrls.Count;
            return allImageUrls.OrderBy(iu => randomGenerator.Next(0, allImageUrlCount))
                .Take(imageCount)
                .ToList();
        }
    }
}
