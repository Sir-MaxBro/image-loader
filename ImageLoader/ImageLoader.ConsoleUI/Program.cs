using ImageLoader.Business.Models;
using ImageLoader.Business.Providers;
using ImageLoader.ConsoleUI.Helpers;
using ImageLoader.ConsoleUI.Helpers.Implementations;
using ImageLoader.IoC;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageLoader.ConsoleUI
{
    class Program
    {
        private readonly IReadHelper _readHelper = new ReadHelper();
        private readonly IPrintHelper _printHelper = new PrintHelper();

        static void Main(string[] args)
        {
            var program = new Program();
            program.StartProgram();
        }

        private void StartProgram()
        {
            _printHelper.PrintText("Please, input image count for load:");
            var imageCount = int.Parse(_readHelper.ReadInputData());

            var images = LoadImagesAsync(imageCount).Result;
            var allEndThreadIds = images.Select(i => i.EndThreadId).Distinct().OrderBy(id => id).ToList();
            var allStartThreadIds = images.Select(i => i.StartThreadId).Distinct().OrderBy(id => id).ToList();

            _printHelper.PrintItems<ImageInfoModel>("Images info:", images, (image) => image.ToString());
            _printHelper.PrintText($"Total count: {images.Count}");
            _printHelper.PrintText($"Total milliseconds: {images.Select(i => i.LoadedTime).Sum()}");
            _printHelper.PrintText($"Total used thread count: {allEndThreadIds.Count + allStartThreadIds.Count}");
            _printHelper.PrintItems<int>("All end threads ids:", allEndThreadIds, (endThreadId) => $"EndThreadId: {endThreadId}");
            _printHelper.PrintItems<int>("All start threads ids:", allStartThreadIds, (startThreadId) => $"StartThreadId: {startThreadId}");
        }

        private async Task<IReadOnlyCollection<ImageInfoModel>> LoadImagesAsync(int imageCount)
        {
            var imageProvider = BootstrapContainer.Container.GetInstance<IImageProvider>();
            return await imageProvider.LoadImagesAsync(imageCount);
        }
    }
}
