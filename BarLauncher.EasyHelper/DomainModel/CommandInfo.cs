using System;

namespace BarLauncher.EasyHelper
{
    public class CommandInfo
    {
        public string Name { get; set; }
        public Func<string> TitleGetter { get; set; }
        public Func<string> SubtitleGetter { get; set; }
        public Action FinalAction { get; set; }
        public ResultGetter ResultGetter { get; set; }

        public string Path { get; set; }
    }
}