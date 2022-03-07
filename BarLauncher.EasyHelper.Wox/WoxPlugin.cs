using BarLauncher.EasyHelper.Core.Service;
using BarLauncher.EasyHelper.Wox.Core.Service;
using BarLauncher.EasyHelper.Wox.Service;
using System;
using System.Collections.Generic;
using Wox.Plugin;

namespace BarLauncher.EasyHelper.Wox
{
    public abstract class WoxPlugin : IPlugin, IDisposable
    {
        protected IBarLauncherContextService BarLauncherContextService { get; set; }
        protected IQueryService QueryService { get; set; }
        protected IResultService ResultService { get; set; }
        protected IBarLauncherResultFinder BarLauncherResultFinder { get; set; }

        public void Init(PluginInitContext context)

        {
            BarLauncherContextService = new BarLauncherContextService(context);
            QueryService = new QueryService();
            ResultService = new ResultService(BarLauncherContextService);
            BarLauncherResultFinder = PrepareContext();
            BarLauncherResultFinder.Init();
        }

        public List<Result> Query(Query query)
        {
            var barLauncherQuery = QueryService.GetBarLauncherQuery(query);
            var results = BarLauncherResultFinder.GetResults(barLauncherQuery);
            return ResultService.MapResults(results);
        }

        public abstract IBarLauncherResultFinder PrepareContext();

        public void Dispose()
        {
            if (BarLauncherResultFinder != null)
            {
                BarLauncherResultFinder.Dispose();
            }
        }
    }
}