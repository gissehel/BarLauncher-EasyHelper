using System;
using BarLauncher.EasyHelper.Core.Service;

namespace BarLauncher.EasyHelper.Service
{
    public abstract class BarLauncherContextServiceBase : IBarLauncherContextService
    {
        public abstract string ActionKeyword { get; }

        public abstract string Seperater { get; }

        public abstract void ChangeQuery(string query);

        public abstract string IconPath { get; }

        public BarLauncherResult GetNoActionResult(string title, string subTitle) => new BarLauncherResult
        {
            Title = title,
            SubTitle = subTitle,
            ShouldClose = false,
        };

        public BarLauncherResult GetActionResult(string title, string subTitle, Action action) => new BarLauncherResult
        {
            Title = title,
            SubTitle = subTitle,
            Action = () =>
            {
                action();
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