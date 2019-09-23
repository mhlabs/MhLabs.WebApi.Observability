using System;

namespace MhLabs.WebApi.Observability.Framework.Logging
{
    public class Settings
    {
        private const string UndefinedStack = "undefined_stack";
        public static string Stack = Environment.GetEnvironmentVariable("mh-stack") ?? UndefinedStack;
        public static bool IsUndefinedStack => Stack == UndefinedStack;

        public static void Init(string stack)
        {
            Stack = stack ?? UndefinedStack;
        }
    }
}