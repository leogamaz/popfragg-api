import { z } from 'zod';

export const userConflictCheckResultSchema = z.object({
  steamIdConflict: z.string().optional(),
  nicknameConflict: z.string().optional(),
  emailConflict: z.string().optional(),
});

export type UserConflictCheckResult = z.infer<typeof userConflictCheckResultSchema>;
