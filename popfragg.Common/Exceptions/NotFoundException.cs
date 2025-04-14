using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace popfragg.Common.Exceptions
{
    public class NotFoundException(string message, string code = "not_found")
    : Exception(message), IHasErrorCode
    {
        public string Code { get; } = code;
    }
}
