using System;

namespace ImageLoader.ConsoleUI.Helpers.Implementations
{
    internal class ReadHelper : IReadHelper
    {
        public string ReadInputData()
        {
            return Console.ReadLine();
        }
    }
}