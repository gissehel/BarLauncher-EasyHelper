using BarLauncher.EasyHelper.Wox.Core.Service;
using Wox.Plugin;

namespace BarLauncher.EasyHelper.Wox.Service
{
    public class QueryService : IQueryService
    {
        public BarLauncherQuery GetBarLauncherQuery(Query pluginQuery)
        {
            var searchTerms = pluginQuery.Search.Split(' ');
            return new BarLauncherQuery
            {
                InternalQuery = pluginQuery,
                RawQuery = pluginQuery.RawQuery,
                Search = pluginQuery.Search,
                SearchTerms = searchTerms,
                Command = pluginQuery.RawQuery.Split(' ')[0],
            };
        }
    }
}