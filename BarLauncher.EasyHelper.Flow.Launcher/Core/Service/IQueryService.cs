using Flow.Launcher.Plugin;

namespace BarLauncher.EasyHelper.Flow.Launcher.Core.Service
{
    public interface IQueryService
    {
        BarLauncherQuery GetBarLauncherQuery(Query pluginQuery);
    }
}