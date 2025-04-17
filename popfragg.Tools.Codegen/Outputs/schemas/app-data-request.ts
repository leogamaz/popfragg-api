import { z } from 'zod';

export const appDataSchema = z.object({
  steamId: z.string().optional(),
});

export type AppDataRequest = z.infer<typeof appDataSchema>;
