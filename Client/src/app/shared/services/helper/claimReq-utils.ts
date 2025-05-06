export const claimReq = {
  adminOnly: (c: any) => c.role == "Admin",
  bossOnly: (c: any) => c.role == "Boss",
}
