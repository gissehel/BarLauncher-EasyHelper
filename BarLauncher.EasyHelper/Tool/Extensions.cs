﻿using System;
using System.Collections.Generic;

namespace BarLauncher.EasyHelper
{
    public static class Extensions
    {
        public static string FormatWith(this string self, params object[] args) => string.Format(self, args);

        public static bool MatchPattern(this string command, string pattern) => string.IsNullOrEmpty(pattern) || command.Contains(pattern);

        public static bool MatchPatternCaseInsensitive(this string command, string pattern) => string.IsNullOrEmpty(pattern) || command.ToLowerInvariant().MatchPattern(pattern.ToLowerInvariant());

        public static T GetAndSetDefault<S, T>(this Dictionary<S, T> self, S key, T defaultValue)
        {
            if (!self.ContainsKey(key))
            {
                self[key] = defaultValue;
            }
            return self[key];
        }

        public static T GetAndSetDefault<S, T>(this Dictionary<S, T> self, S key, Func<T> defaultValueGenerator)
        {
            if (!self.ContainsKey(key))
            {
                self[key] = defaultValueGenerator();
            }
            return self[key];
        }

        public static T GetOrDefault<S, T>(this Dictionary<S, T> self, S key, T defaultValue)
        {
            if (!self.ContainsKey(key))
            {
                return defaultValue;
            }
            return self[key];
        }

        public static T GetOrDefault<S, T>(this Dictionary<S, T> self, S key, Func<T> defaultValueGenerator)
        {
            if (!self.ContainsKey(key))
            {
                return defaultValueGenerator();
            }
            return self[key];
        }
    }
}