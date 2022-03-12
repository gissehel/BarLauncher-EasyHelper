using System;
using System.Collections.Generic;

namespace BarLauncher.EasyHelper.Core.Service
{
    public interface IBarLauncherResultFinder : IDisposable
    {
        IEnumerable<BarLauncherResult> GetResults(BarLauncherQuery barLauncherQuery);

        void Init();
    }
}