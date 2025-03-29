using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Keycloak.AuthServices.Sdk.Admin;
using Keycloak.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using fromshot_api.Common.Repository;
using Supabase;

namespace fromshot_api.Configurations
{
    public static class SupabaseConfig
    {
        public static void AddSupabaseServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Obtendo as configurações necessárias do configuration.
            string supabaseUrl = configuration["Authentication:Supabase:Url"] ?? throw new Exception("Supabase Missing Url");
            string supabaseAnnonKey = configuration["Authentication:Supabase:AnnonKey"] ?? throw new Exception("Supabase Missing Annon Key");
            string supabaseJwtSecret = configuration["Authentication:Supabase:JwtSecret"] ?? throw new Exception("Supabase Missing jwt secret");

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = $"{supabaseUrl}/auth/v1", // Issuer do token
                        ValidateAudience = true,
                        ValidAudience = "authenticated", // Audience conforme o token
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(supabaseJwtSecret)),
                    };
                    options.Authority = supabaseUrl;
                    options.Audience = supabaseUrl;
                    options.RequireHttpsMetadata = false;
                });
            
            var options = new SupabaseOptions
            {
                AutoRefreshToken = true,
                AutoConnectRealtime = true,
                // SessionHandler = new SupabaseSessionHandler() <-- This must be implemented by the developer
            };

            services.AddScoped<Client>(provider =>
            {
                return new Client(supabaseUrl, supabaseAnnonKey);
            });
        }
    }
}