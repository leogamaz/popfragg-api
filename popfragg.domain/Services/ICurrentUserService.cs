using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;

namespace popfragg.Domain.Services
{
    public interface ICurrentUserService
    {
        string? Id { get; }
    }
}
