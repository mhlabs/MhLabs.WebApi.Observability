using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.AspNetCoreServer;
using MhLabs.SerilogExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ecom_order_subscription.Framework.Filters
{
    public class TemplateRouteMetricsFilter : IAsyncResourceFilter
    {
        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            var templateRoute = context.ActionDescriptor?.AttributeRouteInfo?.Template?.ToLowerInvariant();
            var proxyRequest = (APIGatewayProxyRequest) context.HttpContext.Items[AbstractAspNetCoreFunction.LAMBDA_REQUEST_OBJECT];
            proxyRequest?.Headers?.Add(Constants.RouteTemplateHeader, templateRoute);
            LogValueResolver.Register<RouteTemplateEnrichment>(() => templateRoute);

            await next.Invoke();
        }
    }

    public static class TemplateRouteFilterExtensions
    {
        public static void AddTemplateRouteMetrics(this MvcOptions options)
        {
            options.Filters.Add<TemplateRouteMetricsFilter>();
        }
    }
}