using Microsoft.Extensions.Configuration;
using Supabase;

namespace fromshot_api.Common.Repository
{
    public class ConnectionStrings
    {
        protected readonly string SupabaseUrl;
        protected readonly string SupabaseAnnonKey;
        protected readonly string SupabaseJwtSecret;
        protected readonly string ConnectionString;
        protected readonly Client SupabaseClient;
        protected readonly Client SupabaseAdminClient;

        public ConnectionStrings(IConfiguration configuration)
        {
            SupabaseUrl = configuration["Authentication:Supabase:Url"];
            SupabaseAnnonKey = configuration["Authentication:Supabase:AnnonKey"];
            SupabaseJwtSecret = configuration["Authentication:Supabase:JwtSecret"];

            ConnectionString = configuration.GetConnectionString("SupabaseConnection");

            SupabaseClient = new Client(SupabaseUrl, SupabaseAnnonKey);
            SupabaseClient.InitializeAsync().Wait(); // Espera a inicialização ser concluída

            SupabaseAdminClient = new Client(SupabaseUrl, SupabaseJwtSecret);
            SupabaseAdminClient.InitializeAsync().Wait(); // Espera a inicialização ser concluída
        }

        // Métodos públicos para acessar os clients
        public Client GetSupabaseClient() => SupabaseClient;
        public Client GetSupabaseAdminClient() => SupabaseAdminClient;
    }
}
