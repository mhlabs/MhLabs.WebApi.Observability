using System;
using MhLabs.SerilogExtensions;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace MhLabs.WebApi.Observability.Framework.Logging
{
    public static class SerilogFactory
    {
        
        private static ILogger _instance;
        public static ILogger Instance => _instance ?? (_instance = Create());

        internal static ILogger Create()
        {
            Serilog.Debugging.SelfLog.Enable(Console.Error);
            
            return Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.DataProtection", LogEventLevel.Error)
                .Enrich.FromLogContext()
                .Enrich.With<XRayEnrichment>()
                .Enrich.With<MemberIdEnrichment>()
                .Enrich.With<CorrelationIdEnrichment>()
                .Enrich.With<ExceptionEnrichment>()
                .Enrich.With<UserAudienceEnrichment>()
                .Enrich.With<RequestPathEnrichment>()
                .Enrich.With<RouteTemplateEnrichment>()
                .Enrich.WithProperty("Stack", Settings.Stack)
                .WriteTo.Console(new MhLabsCompactJsonFormatter())
                .CreateLogger();
        }

        public static void Set(Logger logger)
        {
            _instance = logger;
        }
    }
}
