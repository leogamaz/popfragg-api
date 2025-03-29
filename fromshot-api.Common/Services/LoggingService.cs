using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Sinks.PostgreSQL;
using System.Collections.Generic;
using Serilog.Filters;
using System;
using Serilog.Events;
using NpgsqlTypes;

namespace fromshot_api.Common.Services
{
    public static class LoggingService
    {
        public static void ConfigureSerilog(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("SupabaseConnection");

            var columnWriters = new Dictionary<string, ColumnWriterBase>
            {
                { "timestamp", new TimestampColumnWriter() },
                { "message", new CustomExceptionMessageColumnWriter() },
                { "exception", new ExceptionColumnWriter() },
                { "properties", new PropertiesColumnWriter() }
            };


            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Error()
                .Filter.ByExcluding(logEvent =>
                    logEvent.Exception is UnauthorizedAccessException // Evita acesso negado para não lotar o banco
                )
                .WriteTo.PostgreSQL(
                    connectionString: connectionString,
                    tableName: "log.error_logs",
                    needAutoCreateTable: true,
                    columnOptions: columnWriters
                )
                .CreateLogger();
        }
    }

    
    public class CustomExceptionMessageColumnWriter : ColumnWriterBase
    {
        public CustomExceptionMessageColumnWriter() : base(NpgsqlDbType.Text) { }

        // Sobrescrever para retornar a mensagem da exceção
        public override object GetValue(LogEvent logEvent, IFormatProvider formatProvider)
        {
            // Retorna a mensagem da exceção, se houver, ou null
            return logEvent.Exception?.Message;
        }
    }
}
