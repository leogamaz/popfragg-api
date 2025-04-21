using popfragg.Domain.DTOS.Steam;
using popfragg.Domain.Interfaces.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace popfragg.Domain.Helpers
{
    public class OpenIdBuildParams : IOpenIdBuildParams
    {

        public FormUrlEncodedContent BuildParamsSteamAuthentication( SteamAuthOpenIdResponse steamParams)
        {
            ArgumentNullException.ThrowIfNull(steamParams);

            if (string.IsNullOrEmpty(steamParams.ClaimedId) ||
                string.IsNullOrEmpty(steamParams.Identity) ||
                string.IsNullOrEmpty(steamParams.ReturnTo) ||
                string.IsNullOrEmpty(steamParams.ResponseNonce) ||
                string.IsNullOrEmpty(steamParams.AssocHandle) ||
                string.IsNullOrEmpty(steamParams.Signed) ||
                string.IsNullOrEmpty(steamParams.Sig) ||
                string.IsNullOrEmpty(steamParams.OpEndpoint) ||
                string.IsNullOrEmpty(steamParams.Ns))
            {
                throw new ArgumentException("Parâmetros Steam inválidos.");
            }

            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "openid.ns", steamParams.Ns },
                { "openid.mode", "check_authentication" },
                { "openid.op_endpoint", steamParams.OpEndpoint},
                { "openid.claimed_id", steamParams.ClaimedId},
                { "openid.identity", steamParams.Identity},
                { "openid.return_to", steamParams.ReturnTo},
                { "openid.response_nonce", steamParams.ResponseNonce},
                { "openid.assoc_handle", steamParams.AssocHandle},
                { "openid.signed", steamParams.Signed},
                { "openid.sig", steamParams.Sig  }
            });

            return content;
        }

        public bool CheckSignaturesOpenId(SteamAuthOpenIdResponse steamParams)
        {
            ArgumentNullException.ThrowIfNull(steamParams);

            if (steamParams.ClaimedId != steamParams.Identity) throw new Exception("Os parâmetros de identidade do Steam não correspondem.");

            return true;

        }
    }
}
