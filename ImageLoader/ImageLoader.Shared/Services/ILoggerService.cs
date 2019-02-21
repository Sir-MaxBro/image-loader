using System;

namespace ImageLoader.Shared.Services
{
    public interface ILoggerService
    {
        void LogError(Exception ex, string message);
    }
}
