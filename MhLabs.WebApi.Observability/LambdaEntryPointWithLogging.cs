using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using MhLabs.APIGatewayLambdaProxy;
using MhLabs.APIGatewayLambdaProxy.Logging;
using MhLabs.SerilogExtensions;
using MhLabs.WebApi.Observability.Framework.Extensions;
using MhLabs.WebApi.Observability.Framework.Logging;
using MhLabs.WebApi.Observability.Framework.Metrics;

namespace MhLabs.WebApi.Observability
{
    public abstract class LambdaEntryPointWithLogging : LambdaEntryPointBase
    {

        public override async Task<APIGatewayProxyResponse> FunctionHandlerAsync(APIGatewayProxyRequest request, ILambdaContext lambdaContext)
        {
            if (string.IsNullOrEmpty(request.HttpMethod)) // due to behaviour of base class LambdaEntryPointBase.cs
            {
                return await base.FunctionHandlerAsync(request, lambdaContext);
            }

            ApplyCorrelationId(request);
            ApplyLogValueResolvers(request);

            return await APIGatewayProxyFunctionLogger.LogFunctionHandlerAsync(request, lambdaContext,
                base.FunctionHandlerAsync, MicrosoftLoggingFactory.Create<APIGatewayProxyFunctionLogger>(), postAction : Performance.Register);
        }

        private static void ApplyLogValueResolvers(APIGatewayProxyRequest request)
        {
            LogValueResolver.Clear();
            LogValueResolver.Register<RouteTemplateEnrichment>(() => request?.Headers?.GetValueOrDefault(Constants.RouteTemplateHeader));
            LogValueResolver.Register<CorrelationIdEnrichment>(() => request?.Headers?.GetValueOrDefault(Constants.CorrelationIdHeader));
            LogValueResolver.Register<MemberIdEnrichment>(() => request?.RequestContext?.Authorizer?.Claims?.GetValueOrDefault("custom:memberid"));
            LogValueResolver.Register<UserAudienceEnrichment>(() => request?.RequestContext?.Authorizer?.Claims?.GetValueOrDefault("aud"));
            LogValueResolver.Register<RequestPathEnrichment>(() => request?.Path);
        }

        
    }


}
