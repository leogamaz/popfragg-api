export const ErrorCodes = {
  ValidationError: 'validation_error',
  BusinessError: 'business_error',
  NotFound: 'not_found',
  InfrastrutctureUnavaliable: 'infraestructure_unavailable',
  PlayerAlreadyInTournament: 'player_already_in_tournament',
  TeamFull: 'team_full',
  UserNotFound: 'user_not_found',
  InvalidSteamId: 'invalid_steam_id',
  SteamIdAlreadyInUse: 'steam_id_already_in_use',
  EmailAlreadyInUse: 'email_already_in_use',
  NicknameAlreadyInUse: 'nickname_already_in_use',
} as const;

export type ErrorCode = typeof ErrorCodes[keyof typeof ErrorCodes];
