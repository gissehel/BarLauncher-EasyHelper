using System.IO;
using BarLauncher.EasyHelper.Core.Service;

namespace BarLauncher.EasyHelper.Service
{
    public class FileReader : IFileReader
    {
        private string Path { get; set; }
        private StreamReader Reader { get; set; }

        public FileReader(string path)
        {
            Path = path;
            Reader = new StreamReader(path);
        }

        public string ReadLine()
        {
            if (Reader != null)
            {
                return Reader.ReadLine();
            }
            return null;
        }

        public void Dispose()
        {
            if (Reader != null)
            {
                Reader.Dispose();
                Reader = null;
            }
        }
    }
}