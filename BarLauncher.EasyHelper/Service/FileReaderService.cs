using System.IO;
using BarLauncher.EasyHelper.Core.Service;

namespace BarLauncher.EasyHelper.Service
{
    public class FileReaderService : IFileReaderService
    {
        public bool FileExists(string path) => File.Exists(path);

        public IFileReader Read(string path)
        {
            return new FileReader(path);
        }
    }
}