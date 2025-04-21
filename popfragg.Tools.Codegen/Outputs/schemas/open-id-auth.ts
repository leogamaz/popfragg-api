import { z } from 'zod';

export const openIdAuthSchema = z.object({
  ns: z.string().optional(),
  mode: z.string().optional(),
  opEndpoint: z.string().optional(),
  claimedId: z.string().optional(),
  identity: z.string().optional(),
  returnTo: z.string().optional(),
  responseNonce: z.string().optional(),
  assocHandle: z.string().optional(),
  signed: z.string().optional(),
  sig: z.string().optional(),
});

export type OpenIdAuth = z.infer<typeof openIdAuthSchema>;
