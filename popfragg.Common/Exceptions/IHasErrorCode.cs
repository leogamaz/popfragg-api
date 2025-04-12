using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fromshot_api.Common.Exceptions
{
    public interface IHasErrorCode
    {
        string Code { get; }
    }
}
