namespace popfragg.Infrastructure.Environment
{
    public class AppSettings
    {
        public ConnectionStrings ConnectionStrings { get; set; } = new();
        public string AspNetCoreEnvironment { get; set; } = "";
        public SerilogSettings Serilog { get; set; } = new();
        public JwtSettings JwtSettings { get; set; } = new();
        public string FrontendOrigin { get; set; } = "http://localhost:4200";
    }
    public class ConnectionStrings
    {
        public string Default { get; set; } = "";
    }
    public class SerilogSettings
    {
        public PostgreSQLSinkSettings PostgreSQL { get; set; } = new(); // Configurações para o sink PostgreSQL
    }

    // classe para as configurações específicas do Serilog.Sinks.PostgreSQL
    public class PostgreSQLSinkSettings
    {
        public string ConnectionString { get; set; } = ""; // String de conexão
        public string TableName { get; set; } = "errors"; // Nome da tabela, padrão "errors"
        public string SchemaName { get; set; } = "logs"; // Nome do schema, padrão "logs"
        public bool NeedAutoCreateTable { get; set; } = true; // Se deve criar a tabela automaticamente
        public bool RespectCase { get; set; } = false; // Se deve respeitar maiúsculas/minúsculas
        // Você pode adicionar outras opções do sink aqui, se necessário, como ColumnOptions
    }
    
    // Jwt Settings
    public class JwtSettings
    {
        public string Key { get; set; } = "";
        public string Issuer { get; set; } = "";
        public string Audience { get; set; } = "";
    }

}
