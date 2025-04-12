using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Sinks.PostgreSQL;

namespace fromshot_api.Configurations
{
    public static class SerilogConfiguration
    {
        public static void ConfigureSerilog(IConfiguration configuration)
        {
            Serilog.Debugging.SelfLog.Enable(msg => Console.WriteLine($"[Serilog SelfLog] {msg}"));
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug() // nível base
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning) // silencia log do ASP.NET Core
                .MinimumLevel.Override("System", LogEventLevel.Warning)    // silencia log do .NET System
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .WriteTo.Console()
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
                        ["trace_id"] = new SinglePropertyColumnWriter("trace_id"),
                        ["path"] = new SinglePropertyColumnWriter("path"),
                        ["extra_field"] = new SinglePropertyColumnWriter("ExtraField")
                    },
                    restrictedToMinimumLevel: LogEventLevel.Error
                )
                .CreateLogger();
        }
    }
}
