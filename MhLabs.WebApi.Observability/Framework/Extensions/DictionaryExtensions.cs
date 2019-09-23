using System.Collections.Generic;

namespace MhLabs.WebApi.Observability.Framework.Extensions
{
    public static class DictionaryExtensions
    {
        public static T GetValueOrDefault<T>(this Dictionary<string, T> dict, string key)
        {
            if (dict.ContainsKey(key))
            {
                return dict[key];
            }
            return default(T);
        }

        public static T GetValueOrDefault<T>(this IDictionary<string, T> dict, string key)
        {
            if (dict.ContainsKey(key))
            {
                return dict[key];
            }
            return default(T);
        }
    }
}