using System;

namespace BarLauncher.EasyHelper.Core.Service
{
    public interface IFileReader : IDisposable
    {
        string ReadLine();
    }
}