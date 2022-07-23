using BarLauncher.EasyHelper.Core.Service;
using BarLauncher.EasyHelper.Flow.Launcher.Core.Service;
using System.Collections.Generic;
using Flow.Launcher.Plugin;

namespace BarLauncher.EasyHelper.Flow.Launcher.Service
{
    public class ResultService : IResultService
    {
        private IBarLauncherContextService BarLauncherContextService { get; set; }

        public ResultService(IBarLauncherContextService barLauncherContextService)
        {
            BarLauncherContextService = barLauncherContextService;
        }

        public List<Result> MapResults(IEnumerable<BarLauncherResult> results)
        {
            var resultList = new List<Result>();
            if (results != null)
            {
                foreach (var result in results)
                {
                    var action = result.Action;
                    resultList.Add(new Result
                    {
                        Title = result.Title,
                        SubTitle = result.SubTitle,
                        IcoPath = result.Icon ?? BarLauncherContextService.IconPath,
                        Action = e =>
                        {
                            if (e.SpecialKeyState.CtrlPressed)
                            {
                                if (result.CtrlAction != null)
                                {
                                    return result.CtrlAction();
                                }
                                return false;
                            }
                            else if (e.SpecialKeyState.WinPressed)
                            {
                                if (result.WinAction != null)
                                {
                                    return result.WinAction();
                                }
                                return false;
                            }
                            else
                            {
                                action?.Invoke();
                                return result.ShouldClose;
                            }
                        }
                    });
                }
            }
            return resultList;
        }
    }
}