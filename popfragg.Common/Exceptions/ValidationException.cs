using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace popfragg.Common.Exceptions
{
    public class ValidationException(string message, string code = "validation_error")
    : Exception(message), IHasErrorCode
    {
        public string Code { get; } = code;
    }
}
