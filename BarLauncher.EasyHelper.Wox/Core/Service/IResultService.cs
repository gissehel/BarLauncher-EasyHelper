using System.Collections.Generic;
using Wox.Plugin;

namespace BarLauncher.EasyHelper.Wox.Core.Service
{
    public interface IResultService
    {
        List<Result> MapResults(IEnumerable<BarLauncherResult> results);
    }
}