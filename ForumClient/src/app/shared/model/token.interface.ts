export interface Token {
  accessToken: string;
  refreshToken: string;
  expiresAt: string
  clientId: string;
}

export interface JwtPayload {
  sub: string;         // Id của user
  name: string;        // UserName
  given_name: string;  // Avatar
  jti: string;         // Token Id
  exp: number;         // Expiry time (timestamp)
  iat: number;         // Issued at (timestamp)
}
