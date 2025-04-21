using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace popfragg.Domain.Interfaces.Repository
{
    public interface ISteamRepository
    {
        public Task<HttpResponseMessage> AuthUser(FormUrlEncodedContent steamParams);
    }
}
