using BarLauncher.EasyHelper.Core.Service;

namespace BarLauncher.EasyHelper.Service
{
    public class FileGeneratorService : IFileGeneratorService
    {
        public IFileGenerator CreateGenerator(string path)
        {
            return new FileGenerator(path);
        }
    }
}