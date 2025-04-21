import { z } from 'zod';

export const signUpDataSchema = z.object({
  signup: z.any().optional(),
});

export type SignUpData = z.infer<typeof signUpDataSchema>;
