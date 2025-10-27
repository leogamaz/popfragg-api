using popfragg.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace popfragg.Infrastructure.Exceptions
{
    public class InfrastructureUnavailableException(
        string message,
        string code = "infrastructure_unavailable"
    ) : AppException(message, 503), IHasErrorCode
    {
        public string Code { get; } = code;
    }
}
