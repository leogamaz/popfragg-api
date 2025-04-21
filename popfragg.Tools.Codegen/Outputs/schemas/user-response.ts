import { z } from 'zod';

export const userResponseSchema = z.object({
  id: z.string().optional(),
  email: z.string().optional(),
  email_Verified: z.boolean(),
  given_Name: z.string().optional(),
  family_Name: z.string().optional(),
  nickname: z.string().optional(),
  preferred_Username: z.string().optional(),
  signup_Methods: z.string().optional(),
  phone_Number_Verified: z.boolean(),
  roles: z.array(z.string()),
  created_At: z.number(),
  updated_At: z.number(),
  app_Data: z.any().optional(),
});

export type UserResponse = z.infer<typeof userResponseSchema>;
