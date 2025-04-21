import { z } from 'zod';

export const signUpSchema = z.object({
  email: z.string().optional(),
  password: z.string().optional(),
  confirmPassword: z.string().optional(),
  nickname: z.string().optional(),
  givenName: z.string(),
  familyName: z.string(),
  gender: z.string().optional(),
  birthdate: z.string().optional(),
  phoneNumber: z.string().optional(),
  roles: z.array(z.string()),
  appData: appDataRequestSchema.optional(),
});

export type SignUpRequest = z.infer<typeof signUpSchema>;
