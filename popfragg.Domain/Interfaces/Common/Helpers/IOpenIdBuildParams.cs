using popfragg.Domain.DTOS.Steam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace popfragg.Domain.Interfaces.Common.Helpers
{
    public interface IOpenIdBuildParams
    {
        public FormUrlEncodedContent BuildParamsSteamAuthentication(SteamAuthOpenIdResponse steamParams);
        public bool CheckSignaturesOpenId(SteamAuthOpenIdResponse steamParams);
    }
}
