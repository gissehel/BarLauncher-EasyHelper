using BarLauncher.EasyHelper.Core.Service;
using BarLauncher.EasyHelper.Wox.Core.Service;
using System.Collections.Generic;
using Wox.Plugin;

namespace BarLauncher.EasyHelper.Wox.Service
{
    public class ResultService : IResultService
    {
        private IBarLauncherContextService WoxContextService { get; set; }

        public ResultService(IBarLauncherContextService woxContextService)
        {
            WoxContextService = woxContextService;
        }

        public List<Result> MapResults(IEnumerable<BarLauncherResult> results)
        {
            var resultList = new List<Result>();
            foreach (var result in results)
            {
                var action = result.Action;
                resultList.Add(new Result
                {
                    Title = result.Title,
                    SubTitle = result.SubTitle,
                    IcoPath = result.Icon ?? WoxContextService.IconPath,
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
            return resultList;
        }
    }
}