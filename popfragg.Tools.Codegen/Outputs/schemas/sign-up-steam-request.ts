import { z } from 'zod';

export const signUpSteamSchema = z.object({
  email: z.string().optional(),
  password: z.string().optional(),
  confirmPassword: z.string().optional(),
  scope: z.any().optional(),
  nickname: z.string().optional(),
  givenName: z.string().optional(),
  familyName: z.string().optional(),
  gender: z.string().optional(),
  birthdate: z.string().optional(),
  phoneNumber: z.string().optional(),
  appData: appDataRequestSchema.optional(),
});

export type SignUpSteamRequest = z.infer<typeof signUpSteamSchema>;
