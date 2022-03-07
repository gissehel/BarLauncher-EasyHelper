using System;

namespace BarLauncher.EasyHelper.Core.Service
{
    public interface IBarLauncherContextService
    {
        void ChangeQuery(string query);

        string ActionKeyword { get; }

        string Seperater { get; }

        string IconPath { get; }

        BarLauncherResult GetNoActionResult(string title, string subTitle);

        BarLauncherResult GetActionResult(string title, string subTitle, Action action);

        BarLauncherResult GetCompletionResult(string title, string subTitle, Func<string> getNewQuery);

        BarLauncherResult GetCompletionResultFinal(string title, string subTitle, Func<string> getNewQuery);
    }
}