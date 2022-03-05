﻿using BarLauncher.EasyHelper.Core.Service;

namespace BarLauncher.EasyHelper.Test.Mock.Service
{
    public class FileGeneratorServiceMock : IFileGeneratorService
    {
        public FileGeneratorMock LastFileGenerator { get; set; }

        public IFileGenerator CreateGenerator(string path)
        {
            LastFileGenerator = new FileGeneratorMock(path);
            return LastFileGenerator;
        }
    }
}