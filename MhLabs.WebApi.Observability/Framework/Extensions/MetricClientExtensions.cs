using Microsoft.Extensions.DependencyInjection;
using mhlabs.metrics;
using MhLabs.WebApi.Observability.Framework.Logging;

namespace MhLabs.WebApi.Observability.Framework.Extensions
{
    public static class MetricClientExtensions
    {
        /// Uses the extracted stack name from executing resource (e.g product-search from product-search-ApiProxy-12312)
        public static IServiceCollection AddMetricsClient(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddMetricsClient(Settings.Stack);
        }
    }
}