using System;
using BarLauncher.EasyHelper.Core.Service;
using Wox.Plugin;

namespace BarLauncher.EasyHelper.Wox.Service
{
    public class BarLauncherContextService : IBarLauncherContextService
    {
        private PluginInitContext Context { get; set; }

        public BarLauncherContextService(PluginInitContext context)
        {
            Context = context;
        }

        public string ActionKeyword => Context.CurrentPluginMetadata.ActionKeyword;

        public string Seperater => Query.TermSeperater;

        public void ChangeQuery(string query) => Context.API.ChangeQuery(query);

        public string IconPath => Context.CurrentPluginMetadata.IcoPath;

        public BarLauncherResult GetActionResult(string title, string subTitle, Action action) => new BarLauncherResult
        {
            Title = title,
            SubTitle = subTitle,
            Action = () =>
            {
                action();
                // ChangeQuery("");
            },
            ShouldClose = true,
        };

        public BarLauncherResult GetCompletionResult(string title, string subTitle, Func<string> getNewQuery) => new BarLauncherResult
        {
            Title = title,
            SubTitle = subTitle,
            Action = () => ChangeQuery(ActionKeyword + Seperater + getNewQuery() + Seperater),
            ShouldClose = false,
        };

        public BarLauncherResult GetCompletionResultFinal(string title, string subTitle, Func<string> getNewQuery) => new BarLauncherResult
        {
            Title = title,
            SubTitle = subTitle,
            Action = () => ChangeQuery(ActionKeyword + Seperater + getNewQuery()),
            ShouldClose = false,
        };
    }
}