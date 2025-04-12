using fromshot_api.Domain.DTOS.Steam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace fromshot_api.Domain.Interfaces.Common.Helpers
{
    public interface IOpenIdBuildParams
    {
        public FormUrlEncodedContent BuildParamsSteamAuthentication(OpenIdAuth steamParams);
        public bool CheckSignaturesOpenId(OpenIdAuth steamParams);
    }
}
