using popfragg.Shared.DTOS.Steam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace popfragg.Shared.Helpers
{
    public static class SteamAuthOpenIdResponseMapper
    {
        public static SteamAuthResponseDTO ToSteamOpenIdResponse(this IQueryCollection query)
        {
            return new SteamAuthResponseDTO
            {
                Ns = query["openid.ns"],
                Mode = query["openid.mode"],
                OpEndpoint = query["openid.op_endpoint"],
                ClaimedId = query["openid.claimed_id"],
                Identity = query["openid.identity"],
                ReturnTo = query["openid.return_to"],
                ResponseNonce = query["openid.response_nonce"],
                AssocHandle = query["openid.assoc_handle"],
                Signed = query["openid.signed"],
                Sig = query["openid.sig"]
            };
        }
    }
}
