using BarLauncher.EasyHelper.Core.Service;
using BarLauncher.EasyHelper.Service;
using Flow.Launcher.Plugin;

namespace BarLauncher.EasyHelper.Flow.Launcher.Service
{
    public class BarLauncherContextService : BarLauncherContextServiceBase, IBarLauncherContextService
    {
        internal PluginInitContext Context { get; set; }

        public BarLauncherContextService(PluginInitContext context) : base()
        {
            Context = context;
        }

        public override string ActionKeyword => Context.CurrentPluginMetadata.ActionKeyword;

        public override string Seperater => Query.TermSeparator;

        public override void ChangeQuery(string query) => Context.API.ChangeQuery(query);

        public override string IconPath => Context.CurrentPluginMetadata.IcoPath;
    }
}