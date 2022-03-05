using Wox.Plugin;

namespace BarLauncher.EasyHelper.Wox.Core.Service
{
    public interface IQueryService
    {
        BarLauncherQuery GetBarLauncherQuery(Query pluginQuery);
    }
}