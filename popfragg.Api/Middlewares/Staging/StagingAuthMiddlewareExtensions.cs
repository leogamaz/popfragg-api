namespace popfragg.Middlewares.Staging
{
    public static class StagingAuthMiddlewareExtensions
    {
        public static IApplicationBuilder UseStagingAuth(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsStaging())
            {
                app.UseMiddleware<StagingAuthMiddleware>();
            }

            return app;
        }
    }
}
