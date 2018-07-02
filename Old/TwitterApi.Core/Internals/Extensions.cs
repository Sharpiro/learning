using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpiroTweeter.Internals
{
    internal static class Extensions
    {
        private const string UnreservedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";

        public static KeyValuePair<TKey, TValue> CreateKvp<TKey, TValue>(TKey tKey, TValue tValue)
        {
            return new KeyValuePair<TKey, TValue>(tKey, tValue);
        }

        public static string UrlEncode(string str)
        {
            if (string.IsNullOrEmpty(str)) throw new ArgumentNullException(nameof(str));
            return str.Select(c => UnreservedChars.Contains(c) ? c.ToString() : '%' + string.Format("{0:X2}", (int)c)).StringJoin("");
        }

        public static string StringJoin(this IEnumerable<string> list, string seperator) => string.Join(seperator, list);
    }
}