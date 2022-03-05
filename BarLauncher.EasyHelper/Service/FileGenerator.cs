using System;
using System.IO;
using BarLauncher.EasyHelper.Core.Service;

namespace BarLauncher.EasyHelper.Service
{
    public class FileGenerator : IFileGenerator, IDisposable
    {
        private StreamWriter Writer { get; set; }
        private string Path { get; set; }

        public FileGenerator(string path)
        {
            Path = path;
            Writer = new StreamWriter(path);
        }

        public IFileGenerator AddLine(string line)
        {
            if (Writer != null)
            {
                Writer.WriteLine(line);
            }
            return this;
        }

        public void Generate()
        {
            if (Writer != null)
            {
                Writer.Close();
                Writer = null;
            }
        }

        public void Dispose()
        {
            if (Writer != null)
            {
                Writer.Close();
                Writer = null;
            }
        }
    }
}