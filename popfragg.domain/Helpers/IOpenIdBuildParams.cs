using popfragg.Shared.DTOS.Steam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace popfragg.Domain.Helpers
{
    public interface IOpenIdBuildParams
    {
        public FormUrlEncodedContent BuildParamsSteamAuthentication(SteamAuthResponseDTO steamParams);
        public bool CheckSignaturesOpenId(SteamAuthResponseDTO steamParams);
    }
}
