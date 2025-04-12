
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using fromshot_api.Common.Configurations;


namespace fromshot_api.Middlewares
{
    public static class Authorizer
    {
        public static void AddAuthorizerService(this IServiceCollection services, EnvironmentConfig connection)
        {
            //Obtendo as configurações necessárias do configuration.
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = connection.AuthorizerUrl;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = connection.AuthorizerUrl,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                    };
                    options.MetadataAddress = connection.AuthorizerJWKS;
                });

            services.AddAuthorization();
        }
    }
}