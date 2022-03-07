using BarLauncher.EasyHelper.Core.Service;
using BarLauncher.EasyHelper.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BarLauncher.EasyHelper.Test.Mock.Service
{
    public class BarLauncherContextServiceMock : BarLauncherContextServiceBase, IBarLauncherContextService
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


        private string _actionKeyword;

        public string SetActionKeyword(string value) => _actionKeyword = value;

        public override string ActionKeyword => this._actionKeyword;

        public override string Seperater => " ";

        public string CurrentQuery { get; set; } = "";

        public override string IconPath => "This is icon path";

        public override void ChangeQuery(string query) => SetCurrentQuery(query);

        public void SetQueryFromInterface(string query)
        {
            SetCurrentQuery(query);
        }

        private void SetCurrentQuery(string query)
        {
            CurrentQuery = query;
            var woxQuery = QueryService.GetBarLauncherQuery(CurrentQuery);
            SetActionKeyword(woxQuery.Command);
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

        public IEnumerable<BarLauncherResult> Results { get; set; } = new List<BarLauncherResult>();

        public bool BarLauncherDisplayed { get; set; } = false;
    }
}