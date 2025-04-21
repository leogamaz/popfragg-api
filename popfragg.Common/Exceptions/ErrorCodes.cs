using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace popfragg.Common.Exceptions
{
    public static class ErrorCodes
    {
        public const string ValidationError = "validation_error";
        public const string BusinessError = "business_error";
        public const string NotFound = "not_found";
        public const string InfrastrutctureUnavaliable = "infraestructure_unavailable";

        // Personalizados:
        public const string PlayerAlreadyInTournament = "player_already_in_tournament";
        public const string TeamFull = "team_full";
        public const string UserNotFound = "user_not_found";
        public const string InvalidSteamId = "invalid_steam_id";
        public const string SteamIdAlreadyInUse = "steam_id_already_in_use";
        public const string EmailAlreadyInUse = "email_already_in_use";
        public const string NicknameAlreadyInUse = "nickname_already_in_use";
    }
}
