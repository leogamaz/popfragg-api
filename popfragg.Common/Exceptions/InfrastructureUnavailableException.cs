using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace popfragg.Common.Exceptions
{
    public class InfrastructureUnavailableException(string message, string code = "infraestructure_unavailable")
    : Exception(message), IHasErrorCode
    {
        public string Code { get; } = code;
    }
}
