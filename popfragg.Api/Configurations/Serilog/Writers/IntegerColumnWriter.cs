using NpgsqlTypes;
using Serilog.Events;
using Serilog.Sinks.PostgreSQL;

namespace popfragg.Configurations.Serilog.Writers
{
    public class IntegerColumnWriter : ColumnWriterBase
    {
        public IntegerColumnWriter() : base(NpgsqlDbType.Integer) { }

        public override object GetValue(LogEvent logEvent, IFormatProvider? formatProvider = null)
        {
            if (logEvent.Properties.TryGetValue("status_code", out var value) &&
                value is ScalarValue scalar &&
                scalar.Value is int intValue)
            {
                return intValue;
            }

            return DBNull.Value;
        }
    }
}
