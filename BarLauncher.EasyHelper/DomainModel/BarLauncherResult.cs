﻿using System;

namespace BarLauncher.EasyHelper
{
    public class BarLauncherResult
    {
        public string Title { get; set; }

        public string SubTitle { get; set; }

        public Action Action { get; set; }

        public string Icon { get; set; }

        public Func<bool> CtrlAction { get; set; }

        public Func<bool> WinAction { get; set; }

        public bool ShouldClose { get; set; } = true;
    }
}