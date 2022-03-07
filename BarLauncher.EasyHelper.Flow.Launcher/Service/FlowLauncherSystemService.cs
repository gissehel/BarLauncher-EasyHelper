using BarLauncher.EasyHelper.Core.Service;
using BarLauncher.EasyHelper.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarLauncher.EasyHelper.Flow.Launcher.Service
{
    public class FlowLauncherSystemService : SystemService
    {
        private BarLauncherContextService BarLauncherContextService { get; set; }
        public FlowLauncherSystemService(string applicationName, BarLauncherContextService barLauncherContextService) : base(applicationName)
        {
            BarLauncherContextService = barLauncherContextService;
        }

        public override void OpenUrl(string url) => BarLauncherContextService.Context.API.OpenUrl(url);

        public override void CopyTextToClipboard(string text) => BarLauncherContextService.Context.API.CopyToClipboard(text);

        public override void OpenDirectory(string directory) => BarLauncherContextService.Context.API.OpenDirectory(directory);
    }
}
