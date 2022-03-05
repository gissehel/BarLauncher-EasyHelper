using System.Linq;

namespace BarLauncher.EasyHelper.Test.Mock.Service
{
    public class QueryServiceMock
    {
        public BarLauncherQuery GetBarLauncherQuery(string internalQuery)
        {
            var elements = internalQuery.Trim(' ').Split(' ').Where(term => term.Length > 0).ToArray();
            var searchTerms = elements.Skip(1).ToArray();
            var rawQuery = string.Join(" ", elements);
            var search = string.Join(" ", searchTerms);

            return new BarLauncherQuery
            {
                InternalQuery = internalQuery,
                RawQuery = rawQuery,
                Search = search,
                SearchTerms = searchTerms,
                Command = elements.Length > 0 ? elements[0] : string.Empty,
            };
        }
    }
}