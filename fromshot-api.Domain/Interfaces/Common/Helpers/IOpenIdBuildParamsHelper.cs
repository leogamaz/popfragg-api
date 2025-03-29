using fromshot_api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace fromshot_api.Domain.Interfaces.Common.Helpers
{
    public interface IOpenIdBuildParamsHelper
    {
        public FormUrlEncodedContent BuildParamsSteamAuthentication(OpenIdAuthModel steamParams);
        public bool CheckSignaturesOpenId(OpenIdAuthModel steamParams);
    }
}
