using popfragg.Infrastructure.Environment;
using popfragg.Infrastructure.Exceptions;
using Serilog;
using Serilog.Debugging;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Sinks.PostgreSQL;
using System.ComponentModel.DataAnnotations;

namespace popfragg.Api.Configurations.Serilog
{
    public class SerilogConfiguration
    {
        public static void ConfigureSerilog(IConfiguration configuration)
        {
            global::Serilog.Debugging.SelfLog.Enable(msg => Console.WriteLine($"[Serilog SelfLog] {msg}"));
            var appSettings = configuration.Get<AppSettings>();
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("System", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .WriteTo.Console()
            .Filter.ByExcluding(logEvent => ShouldIgnoreException(logEvent.Exception))
            .WriteTo.PostgreSQL(
                connectionString: appSettings!.Serilog.PostgreSQL.ConnectionString,
                tableName: appSettings!.Serilog.PostgreSQL.TableName,
                schemaName: appSettings!.Serilog.PostgreSQL.SchemaName,
                needAutoCreateTable: appSettings.Serilog.PostgreSQL.NeedAutoCreateTable,
                respectCase: appSettings.Serilog.PostgreSQL.RespectCase,
                useCopy: false, // copy causa erro de encoding no PostgreSQL às vezes
                columnOptions: new Dictionary<string, ColumnWriterBase>
                {
                    ["created_at"] = new TimestampColumnWriter(),            // timestamp do log
                    ["level"] = new LevelColumnWriter(),                     // nível
                    ["message"] = new RenderedMessageColumnWriter(),         // mensagem principal
                    ["stack_trace"] = new ExceptionColumnWriter(),           // detalhes do erro
                    ["path"] = new SinglePropertyColumnWriter("path", writeMethod: PropertyWriteMethod.Raw),
                    ["method"] = new SinglePropertyColumnWriter("method", writeMethod: PropertyWriteMethod.Raw),
                    ["user_id"] = new NullableSinglePropertyColumnWriter("user_id"),
                    ["http_status"] = new IntegerColumnWriter(),             // status_code → http_status
                    ["trace_id"] = new SinglePropertyColumnWriter("trace_id", writeMethod: PropertyWriteMethod.Raw),
                    ["message_user"] = new SinglePropertyColumnWriter("message_user", writeMethod: PropertyWriteMethod.Raw),
                    ["code_message"] = new SinglePropertyColumnWriter("code_message", writeMethod: PropertyWriteMethod.Raw),
                    ["properties"] = new LogEventSerializedColumnWriter()    // serialização completa do log
                },
                restrictedToMinimumLevel: LogEventLevel.Error
            )
            .CreateLogger();
        }

        private static bool ShouldIgnoreException(Exception? ex)
        {
            return ex is BusinessException or ValidationException or UnauthorizedAccessException;
        }
    }
}
