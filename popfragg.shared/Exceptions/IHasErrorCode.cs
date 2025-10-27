using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace popfragg.Shared.Exceptions
{
    public interface IHasErrorCode
    {
        string Code { get; }
    }
}
