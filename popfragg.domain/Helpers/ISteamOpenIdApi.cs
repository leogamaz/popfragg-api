using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace popfragg.Domain.Helpers
{
    public interface ISteamOpenIdApi
    {
        public Task<HttpResponseMessage> AuthUser(FormUrlEncodedContent steamParams);
    }
}

