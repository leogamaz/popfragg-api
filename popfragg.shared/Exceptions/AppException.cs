using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace popfragg.Shared.Exceptions
{
    public abstract class AppException : Exception
    {
        public int? StatusCode { get; }

        protected AppException(string message, int? statusCode = null)
            : base(message)
        {
            StatusCode = statusCode;
        }

        protected AppException(string message, Exception? innerException, int? statusCode = null)
            : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }
}
