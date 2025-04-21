import { z } from 'zod';

export const appDataResponseSchema = z.object({
  email: z.string().optional(),
  roles: z.array(z.string()),
  steam_Id: z.string().optional(),
});

export type AppDataResponse = z.infer<typeof appDataResponseSchema>;
