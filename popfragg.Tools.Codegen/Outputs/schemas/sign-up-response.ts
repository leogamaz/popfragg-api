import { z } from 'zod';

export const signUpResponseSchema = z.object({
  message: z.string().optional(),
  access_Token: z.string().optional(),
  expires_In: z.number(),
  user: z.any().optional(),
});

export type SignUpResponse = z.infer<typeof signUpResponseSchema>;
