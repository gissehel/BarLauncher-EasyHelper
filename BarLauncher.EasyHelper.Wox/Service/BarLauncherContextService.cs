using BarLauncher.EasyHelper.Core.Service;
using BarLauncher.EasyHelper.Service;
using Wox.Plugin;

namespace BarLauncher.EasyHelper.Wox.Service
{
    public class BarLauncherContextService : BarLauncherContextServiceBase, IBarLauncherContextService
    {
        private PluginInitContext Context { get; set; }

        public BarLauncherContextService(PluginInitContext context)
        {
            Context = context;
        }

        public override string ActionKeyword => Context.CurrentPluginMetadata.ActionKeyword;

        public override string Seperater => Query.TermSeperater;

        public override void ChangeQuery(string query) => Context.API.ChangeQuery(query);

        public override string IconPath => Context.CurrentPluginMetadata.IcoPath;
    }
}