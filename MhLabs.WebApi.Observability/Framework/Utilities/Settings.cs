using System;
using System.Linq;
using System.Text.RegularExpressions;
using MhLabs.Extensions;

namespace MhLabs.WebApi.Observability.Framework.Logging
{
    public class Settings
    {
        private const string UndefinedStack = "undefined_stack";
        public static string Stack = GetStackName();
        public static bool IsUndefinedStack => Stack == UndefinedStack;

        public static void Init(string stack)
        {
            Stack = stack ?? UndefinedStack;
        }

        public static string GetStackName()
        {
            const string LAMBDA_NAME = "AWS_LAMBDA_FUNCTION_NAME";
            const string REGEX = @"(?<!^)(?=[A-Z])";

            var env = Env.Get(LAMBDA_NAME);
            if(env == null) return UndefinedStack;

            return Regex.Split(env, REGEX).FirstOrDefault()?.TrimEnd('-') ?? UndefinedStack;
            
        }
    }
}