import { z } from 'zod';

export const graphQLResponseSchema = z.object({
  data: z.any().optional(),
  errors: z.array(graphQLErrorSchema).optional(),
});

export type GraphQLResponse = z.infer<typeof graphQLResponseSchema>;
