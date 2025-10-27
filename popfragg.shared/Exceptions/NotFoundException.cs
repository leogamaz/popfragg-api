using popfragg.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace popfragg.Infrastructure.Exceptions
{
    public class NotFoundException(
        string message,
        string code = "not_found"
    ) : AppException(message, 404), IHasErrorCode
    {
        public string Code { get; } = code;
    }
}
