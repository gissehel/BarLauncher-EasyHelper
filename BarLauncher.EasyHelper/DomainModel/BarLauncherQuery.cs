﻿using System.Collections.Generic;
using System.Linq;

namespace BarLauncher.EasyHelper
{
    public class BarLauncherQuery
    {
        public object InternalQuery { get; set; }

        public string RawQuery { get; set; }

        public string Search { get; set; }

        public string[] SearchTerms { get; set; }

        public string Command { get; set; }

        public string FirstTerm => SearchTerms.Length > 0 ? SearchTerms[0] : string.Empty;

        public string GetTermOrEmpty(int index) => (SearchTerms.Length > index) ? SearchTerms[index] : string.Empty;

        public IEnumerable<string> GetSearchTermsStarting(int index) => SearchTerms.Skip(index);

        public string GetAllSearchTermsStarting(int index) => (SearchTerms.Length > index) ? string.Join(" ", SearchTerms.Skip(index).ToArray()) : null;
    }
}