using ImageLoader.Business.Constants;
using ImageLoader.Business.Models;
using ImageLoader.Business.Services;
using ImageLoader.Shared.Configurations;
using ImageLoader.Shared.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ImageLoader.Business.Providers.Implementations
{
    internal class ImageProvider : IImageProvider
    {
        private readonly IImageService _imageService;
        private readonly ILoggerService _loggerService;
        private readonly IImageSourceProvider _imageSourceProvider;
        private readonly IConfigurationProvider _configurationProvider;

        public ImageProvider(IImageService imageService,
            ILoggerService loggerService,
            IImageSourceProvider imageSourceProvider,
            IConfigurationProvider configurationProvider)
        {
            _imageService = imageService ?? throw new ArgumentNullException(nameof(imageService));
            _loggerService = loggerService ?? throw new ArgumentNullException(nameof(loggerService));
            _imageSourceProvider = imageSourceProvider ?? throw new ArgumentNullException(nameof(imageSourceProvider));
            _configurationProvider = configurationProvider ?? throw new ArgumentNullException(nameof(configurationProvider));
        }

        public async Task<IReadOnlyCollection<ImageInfoModel>> LoadImagesAsync(int imageCount = ImagesConstants.DefaultImageCount)
        {
            var imageUrls = _imageSourceProvider.GetRandomImageUrls(imageCount);
            var taskCount = this.GetNeededTaskCountByImageCount(imageUrls.Count);
            var tasks = new List<Task>(taskCount);
            var imagesPerTask = this.CalculateImagesPerTask(imageUrls.Count, taskCount);
            for (var taskIndex = 0; taskIndex < taskCount; taskIndex++)
            {
                var skipCount = imagesPerTask * taskIndex;
                var currentImageUrls = imageUrls.Skip(skipCount).Take(imagesPerTask).ToList();
                var task = Task.Run(async () =>
                {
                    var images = await this.GetImagesByUrlsAsync(currentImageUrls);
                    _imageService.SaveRange(images);
                });
                tasks.Add(task);
            }

            await Task.WhenAll(tasks);

            return _imageService.GetAll();
        }

        private async Task<IReadOnlyCollection<ImageInfoModel>> GetImagesByUrlsAsync(IReadOnlyCollection<string> imageUrls)
        {
            var loadedImages = new List<ImageInfoModel>();
            using (var httpClient = new HttpClient())
            {
                foreach (var imageUrl in imageUrls)
                {
                    var stopWatch = default(Stopwatch);
                    var startThreadId = this.GetCurrentThreadId();
                    var responseMessage = default(HttpResponseMessage);

                    try
                    {
                        stopWatch = Stopwatch.StartNew();
                        responseMessage = await httpClient.GetAsync(imageUrl);
                    }
                    catch (HttpRequestException ex)
                    {
                        // log exception
                        _loggerService.LogError(ex, $"Image url: {imageUrl}");
                    }
                    catch (AggregateException ex)
                    {
                        // log exception
                        _loggerService.LogError(ex, $"Image url: {imageUrl}");
                    }
                    finally
                    {
                        stopWatch?.Stop();
                    }

                    if (responseMessage == null)
                    {
                        continue;
                    }

                    var imageBytes = await responseMessage.Content.ReadAsByteArrayAsync();
                    var imageInfoModel = new ImageInfoModel(imageUrl, imageBytes)
                    {
                        LoadedTime = stopWatch.ElapsedMilliseconds,
                        EndThreadId = this.GetCurrentThreadId(),
                        StartThreadId = startThreadId,
                    };

                    loadedImages.Add(imageInfoModel);
                }
            }

            return loadedImages;
        }

        private int GetNeededTaskCountByImageCount(int imageCount)
        {
            var taskCount = _configurationProvider.GetSystemCoreCount();
            if (imageCount < taskCount)
            {
                return imageCount;
            }
            return taskCount;
        }

        private int CalculateImagesPerTask(int imageCount, int taskCount)
        {
            return (int)Math.Ceiling((double)imageCount / taskCount);
        }

        private int GetCurrentThreadId() => Thread.CurrentThread.ManagedThreadId;
    }
}
