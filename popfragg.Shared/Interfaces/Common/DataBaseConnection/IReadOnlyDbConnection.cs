using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace popfragg.Domain.Interfaces.Common.DataBaseConnection
{
    public interface IReadOnlyDbConnection
    {
        Task<T> WithConnection<T>(Func<IDbConnection, Task<T>> action);
        Task WithConnection(Func<IDbConnection, Task> action);
    }
}
