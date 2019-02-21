using System;
using System.Collections.Generic;

namespace ImageLoader.ConsoleUI.Helpers.Implementations
{
    internal class PrintHelper : IPrintHelper
    {
        public void PrintItems<T>(string header, IReadOnlyCollection<T> items, Func<T, string> getPrintedItem)
        {
            Console.WriteLine();
            Console.WriteLine(header);
            foreach (var item in items)
            {
                Console.WriteLine(getPrintedItem(item));
            }
        }

        public void PrintText(string text)
        {
            Console.WriteLine(text);
        }
    }
}
