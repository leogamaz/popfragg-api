using Serilog.Events;
using Serilog.Sinks.PostgreSQL;

namespace popfragg.Api.Configurations.Serilog
{
    public class NullableSinglePropertyColumnWriter : ColumnWriterBase
    {
        private readonly string _propertyName;

        public NullableSinglePropertyColumnWriter(string propertyName)
            : base(NpgsqlTypes.NpgsqlDbType.Uuid) // tipo da coluna
        {
            _propertyName = propertyName;
        }

        public override object GetValue(LogEvent logEvent, IFormatProvider? formatProvider = null)
        {
            if (logEvent.Properties.TryGetValue(_propertyName, out var value) &&
                value is ScalarValue scalar &&
                scalar.Value is Guid guidValue)
            {
                return guidValue;
            }

            return DBNull.Value; // <- retorna DBNull em vez de lançar erro
        }
    }
}
