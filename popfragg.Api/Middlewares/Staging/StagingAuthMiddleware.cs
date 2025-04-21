using popfragg.Infrastructure.Configurations;

namespace popfragg.Middlewares.Staging
{
    public class StagingAuthMiddleware(RequestDelegate next,EnvironmentConfig environmentConfig)
    {
        private readonly RequestDelegate _next = next;
        private readonly string? _expectedToken = environmentConfig.StagingAuthToken;

        public async Task InvokeAsync(HttpContext context)
        {
            var receivedToken = context.Request.Headers["X-Staging-Auth"].ToString();

            if (string.IsNullOrEmpty(_expectedToken) || receivedToken != _expectedToken)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized");
                return;
            }

            await _next(context);
        }
    }

}
