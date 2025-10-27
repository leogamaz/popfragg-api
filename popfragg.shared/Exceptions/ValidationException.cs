using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace popfragg.Shared.Exceptions
{
    public class ValidationException(
        string message,
        string code = "validation_error"
    ) : AppException(message, 422), IHasErrorCode
    {
        public string Code { get; } = code;
    }
}
