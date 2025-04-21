using popfragg.Domain.DTOS.Steam;

namespace popfragg.Helper.Mappers
{
    public static class SteamAuthOpenIdResponseMapper
    {
        public static SteamAuthOpenIdResponse ToSteamOpenIdResponse(this IQueryCollection query)
        {
            return new SteamAuthOpenIdResponse
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
