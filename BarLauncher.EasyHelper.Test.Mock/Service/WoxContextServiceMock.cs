using BarLauncher.EasyHelper.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BarLauncher.EasyHelper.Test.Mock.Service
{
    public class BarLauncherContextServiceMock : IBarLauncherContextService
    {
        private QueryServiceMock QueryService { get; set; }

        private Dictionary<string, IBarLauncherResultFinder> BarLauncherResultFinderByCommandName { get; set; } = new Dictionary<string, IBarLauncherResultFinder>();

        public BarLauncherContextServiceMock(QueryServiceMock queryService)
        {
            QueryService = queryService;
        }

        public void AddQueryFetcher(string commandName, IBarLauncherResultFinder queryFetcher)
        {
            BarLauncherResultFinderByCommandName[commandName] = queryFetcher;
        }

        public string ActionKeyword { get; set; }

        public string Seperater => " ";

        public string CurrentQuery { get; set; } = "";

        public string IconPath => "This is icon path";

        public void ChangeQuery(string query)
        {
            SetCurrentQuery(query);
        }

        public void SetQueryFromInterface(string query)
        {
            SetCurrentQuery(query);
        }

        private void SetCurrentQuery(string query)
        {
            CurrentQuery = query;
            var woxQuery = QueryService.GetBarLauncherQuery(CurrentQuery);
            ActionKeyword = woxQuery.Command;
            if (BarLauncherResultFinderByCommandName.ContainsKey(woxQuery.Command))
            {
                var results = BarLauncherResultFinderByCommandName[woxQuery.Command].GetResults(woxQuery);
                if (results == null)
                {
                    Results = new List<BarLauncherResult>();
                }
                else
                {
                    Results = results.Where(result => result != null);
                }
            }
            else
            {
                Results = new List<BarLauncherResult>();
            }
        }

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

        public IEnumerable<BarLauncherResult> Results { get; set; } = new List<BarLauncherResult>();

        public bool BarLauncherDisplayed { get; set; } = false;
    }
}