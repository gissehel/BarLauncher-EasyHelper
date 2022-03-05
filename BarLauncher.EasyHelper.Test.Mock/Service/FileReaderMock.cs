using BarLauncher.EasyHelper.Core.Service;
using System.Collections.Generic;

namespace BarLauncher.EasyHelper.Test.Mock.Service
{
    internal class FileReaderMock : IFileReader
    {
        public string Path { get; set; }
        public List<string> Lines { get; private set; }
        public int CurrentLineIndex { get; set; } = 0;

        public FileReaderMock(string path, List<string> lines)
        {
            Path = path;
            Lines = lines;
        }

        public string ReadLine()
        {
            if (Lines != null)
            {
                if (CurrentLineIndex >= 0 && CurrentLineIndex < Lines.Count)
                {
                    var currentLineIndex = CurrentLineIndex;
                    CurrentLineIndex++;
                    return Lines[currentLineIndex];
                }
            }
            return null;
        }

        public void Dispose()
        {
            Lines = null;
        }
    }
}