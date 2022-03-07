using System.Collections.Generic;
using Flow.Launcher.Plugin;

namespace BarLauncher.EasyHelper.Flow.Launcher.Core.Service
{
    public interface IResultService
    {
        List<Result> MapResults(IEnumerable<BarLauncherResult> results);
    }
}