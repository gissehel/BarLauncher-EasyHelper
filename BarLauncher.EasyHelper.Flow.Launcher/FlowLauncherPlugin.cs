using BarLauncher.EasyHelper.Core.Service;
using BarLauncher.EasyHelper.Flow.Launcher.Core.Service;
using BarLauncher.EasyHelper.Flow.Launcher.Service;
using Flow.Launcher.Plugin;
using System;
using System.Collections.Generic;

namespace BarLauncher.EasyHelper.Flow.Launcher
{
    public abstract class FlowLauncherPlugin : IPlugin, IDisposable
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