﻿namespace BarLauncher.EasyHelper.Core.Service
{
    public interface IFileGeneratorService
    {
        IFileGenerator CreateGenerator(string path);
    }
}