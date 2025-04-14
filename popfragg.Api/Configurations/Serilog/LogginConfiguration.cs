using popfragg.Common.Exceptions;
using popfragg.Configurations.Serilog.Writers;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Sinks.PostgreSQL;


namespace popfragg.Configurations.Serilog
{
    public static class SerilogConfiguration
    {
        public static void ConfigureSerilog(IConfiguration configuration)
        {
            global::Serilog.Debugging.SelfLog.Enable(msg => Console.WriteLine($"[Serilog SelfLog] {msg}"));
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug() // nível base
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning) // silencia log do ASP.NET Core
                .MinimumLevel.Override("System", LogEventLevel.Warning)    // silencia log do .NET System
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .WriteTo.Console()
                .Filter.ByExcluding(logEvent => ShouldIgnoreException(logEvent.Exception))
                .WriteTo.PostgreSQL(
                    connectionString: configuration.GetConnectionString("WriteDataBase"),
                    tableName: "log.logs",
                    needAutoCreateTable: false,
                    useCopy: false, //Sem isso da erro de enconding no level, entao usa insert ao inves de copy
                    columnOptions: new Dictionary<string, ColumnWriterBase>
                    {
                        ["timestamp"] = new TimestampColumnWriter(),
                        ["level"] = new LevelColumnWriter(),
                        ["message"] = new RenderedMessageColumnWriter(),
                        ["message_template"] = new MessageTemplateColumnWriter(),
                        ["exception"] = new ExceptionColumnWriter(),
                        ["properties"] = new LogEventSerializedColumnWriter(),
                        ["trace_id"] = new SinglePropertyColumnWriter("trace_id", writeMethod: PropertyWriteMethod.Raw),
                        ["path"] = new SinglePropertyColumnWriter("path", writeMethod: PropertyWriteMethod.Raw),
                        ["message_user"] = new SinglePropertyColumnWriter("message_user", writeMethod: PropertyWriteMethod.Raw),
                        ["code_message"] = new SinglePropertyColumnWriter("code_message", writeMethod: PropertyWriteMethod.Raw),
                        ["status_code"] = new IntegerColumnWriter(),
                    },
                    restrictedToMinimumLevel: LogEventLevel.Error
                )
                .CreateLogger();
        }

        private static bool ShouldIgnoreException(Exception? ex)
        {
            return ex is BusinessException 
                || ex is ValidationException;
        }
    }
}
