import { z } from 'zod';

export const graphQLErrorSchema = z.object({
  message: z.string().optional(),
  path: z.array(z.string()).optional(),
});

export type GraphQLError = z.infer<typeof graphQLErrorSchema>;
