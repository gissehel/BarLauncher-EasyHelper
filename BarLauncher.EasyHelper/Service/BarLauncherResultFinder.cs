﻿using System;
using System.Collections.Generic;
using BarLauncher.EasyHelper.Core.Service;

namespace BarLauncher.EasyHelper
{
    public abstract class BarLauncherResultFinder : IBarLauncherResultFinder
    {
        protected IBarLauncherContextService BarLauncherContextService { get; set; }

        public BarLauncherResultFinder(IBarLauncherContextService barLauncherContextService)
        {
            BarLauncherContextService = barLauncherContextService;
        }

        public virtual void Init()
        {
        }

        protected BarLauncherResult GetNoActionResult(string title, string subTitle) => new BarLauncherResult
        {
            Title = title,
            SubTitle = subTitle,
            ShouldClose = false,
        };

        protected BarLauncherResult GetActionResult(string title, string subTitle, Action action) => new BarLauncherResult
        {
            Title = title,
            SubTitle = subTitle,
            Action = () =>
            {
                action();
                // ChangeQuery("");
            },
            ShouldClose = true,
        };

        protected BarLauncherResult GetCompletionResult(string title, string subTitle, Func<string> getNewQuery) => new BarLauncherResult
        {
            Title = title,
            SubTitle = subTitle,
            Action = () => BarLauncherContextService.ChangeQuery(BarLauncherContextService.ActionKeyword + BarLauncherContextService.Seperater + getNewQuery() + BarLauncherContextService.Seperater),
            ShouldClose = false,
        };

        protected BarLauncherResult GetCompletionResultFinal(string title, string subTitle, Func<string> getNewQuery) => new BarLauncherResult
        {
            Title = title,
            SubTitle = subTitle,
            Action = () => BarLauncherContextService.ChangeQuery(BarLauncherContextService.ActionKeyword + BarLauncherContextService.Seperater + getNewQuery()),
            ShouldClose = false,
        };

        public virtual IEnumerable<BarLauncherResult> GetResults(BarLauncherQuery query) => MatchCommands(query, 0, CommandInfos, string.Empty);

        protected List<CommandInfo> CommandInfos => GetCommandInfos(string.Empty);

        protected Dictionary<string, List<CommandInfo>> CommandInfosByPath { get; set; } = new Dictionary<string, List<CommandInfo>>();
        protected Dictionary<string, CommandInfo> DefaultCommandInfoByPath { get; set; } = new Dictionary<string, CommandInfo>();

        protected void AddDefaultCommand(ResultGetter func)
            => AddDefaultCommand(func, string.Empty);

        protected void AddDefaultCommand(ResultGetter func, string path)
            => AddCommand(null, null, null, null, func, path);

        protected void AddCommand(string name, string title, string subtitle, ResultGetter func)
            => AddCommand(name, title, subtitle, func, string.Empty);

        protected void AddCommand(string name, string title, string subtitle, ResultGetter func, string path)
            => AddCommand(name, title, subtitle, null, func, path);

        protected void AddCommand(string name, string title, string subtitle, Action action)
            => AddCommand(name, title, subtitle, action, string.Empty);

        protected void AddCommand(string name, string title, string subtitle, Action action, string path)
            => AddCommand(name, title, subtitle, action, null, path);

        private void AddCommand(string name, string title, string subtitle, Action action, ResultGetter func, string path)
        {
            var actualPath = string.IsNullOrEmpty(path) ? string.Empty : path;
            if (name == null)
            {
                DefaultCommandInfoByPath[actualPath] = new CommandInfo { ResultGetter = func };
            }
            else
            {
                var commandInfo = new CommandInfo { Name = name, Title = title, Subtitle = subtitle, FinalAction = action, ResultGetter = func, Path = actualPath };
                GetCommandInfos(actualPath).Add(commandInfo);
            }
        }

        protected List<CommandInfo> GetCommandInfos(string path) => CommandInfosByPath.GetAndSetDefault(path, () => new List<CommandInfo>());

        protected CommandInfo GetDefaultCommandInfo(string path) => DefaultCommandInfoByPath.GetOrDefault(path, null as CommandInfo);

        protected BarLauncherResult GetEmptyCommandResult(string commandName, IEnumerable<CommandInfo> commandInfos)
        {
            foreach (var commandInfo in commandInfos)
            {
                if (commandName == commandInfo.Name)
                {
                    return GetEmptyCommandResult(commandInfo);
                }
            }
            return null;
        }

        private BarLauncherResult GetEmptyCommandResult(CommandInfo commandInfo)
        {
            if (commandInfo != null)
            {
                if (commandInfo.FinalAction != null)
                {
                    return GetActionResult(commandInfo.Title, commandInfo.Subtitle, commandInfo.FinalAction);
                }
                else
                {
                    return GetCompletionResult(commandInfo.Title, commandInfo.Subtitle, () => string.IsNullOrEmpty(commandInfo.Path) ? commandInfo.Name : commandInfo.Path + BarLauncherContextService.Seperater + commandInfo.Name);
                }
            }
            return null;
        }

        protected IEnumerable<BarLauncherResult> MatchCommands(BarLauncherQuery query, int position, IEnumerable<CommandInfo> commandInfos, string path)
        {
            var results = new List<BarLauncherResult>();
            var term = query.GetTermOrEmpty(position);
            foreach (var commandInfo in commandInfos)
            {
                var commandName = commandInfo.Name;
                var newPath = commandName;
                if (!string.IsNullOrEmpty(path))
                {
                    newPath = path + BarLauncherContextService.Seperater + commandName;
                }
                if (commandName.MatchPattern(term))
                {
                    if (term == commandName)
                    {
                        if (commandInfo.FinalAction != null)
                        {
                            results.Add(GetActionResult(commandInfo.Title, commandInfo.Subtitle, commandInfo.FinalAction));
                        }
                        else if (commandInfo.ResultGetter != null)
                        {
                            var subCommandResults = commandInfo.ResultGetter(query, position + 1);
                            if (subCommandResults != null)
                            {
                                results.AddRange(subCommandResults);
                            }
                        }
                        else
                        {
                            var subCommandResults = MatchCommands(query, position + 1, GetCommandInfos(newPath), newPath);
                            if (subCommandResults != null)
                            {
                                results.AddRange(subCommandResults);
                            }
                        }
                    }
                    else
                    {
                        if (query.SearchTerms.Length <= position + 1)
                        {
                            var result = GetEmptyCommandResult(commandInfo);
                            if (result != null)
                            {
                                results.Add(result);
                            }
                        }
                    }
                }
            }
            if (results.Count == 0)
            {
                var commandInfo = GetDefaultCommandInfo(path);
                if (commandInfo != null)
                {
                    var commandResults = commandInfo.ResultGetter(query, position);
                    if (commandResults != null)
                    {
                        results.AddRange(commandResults);
                    }
                }
                else
                {
                    results.Add(GetEmptyCommandResult(path, commandInfos));
                }
            }
            return results;
        }

        public virtual void Dispose()
        {
        }
    }
}