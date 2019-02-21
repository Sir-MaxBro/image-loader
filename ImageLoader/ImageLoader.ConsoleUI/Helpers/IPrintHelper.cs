using System;
using System.Collections.Generic;

namespace ImageLoader.ConsoleUI.Helpers
{
    public interface IPrintHelper
    {
        void PrintItems<T>(string header, IReadOnlyCollection<T> items, Func<T, string> getPrintedItem);

        void PrintText(string text);
    }
}
