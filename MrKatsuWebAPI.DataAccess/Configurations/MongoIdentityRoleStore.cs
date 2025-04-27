using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;

namespace MrKatsuWebAPI.DataAccess.Configurations
{
    public class MongoIdentityRoleStore : IRoleStore<IdentityRole>
    {
        private readonly IMongoCollection<IdentityRole> _roles;

        public MongoIdentityRoleStore(MongoDbContext context)
        {
            _roles = context.Database.GetCollection<IdentityRole>("Roles");
        }

        public async Task<IdentityResult> CreateAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            await _roles.InsertOneAsync(role, null, cancellationToken);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            var result = await _roles.DeleteOneAsync(r => r.Id == role.Id, cancellationToken);
            return result.DeletedCount > 0 ? IdentityResult.Success : IdentityResult.Failed(new IdentityError { Description = "Role not found" });
        }

        public async Task<IdentityRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            return await _roles.Find(r => r.Id == roleId).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IdentityRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            return await _roles.Find(r => r.NormalizedName == normalizedRoleName).FirstOrDefaultAsync(cancellationToken);
        }

        public Task<string> GetRoleIdAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Id);
        }

        public Task<string> GetRoleNameAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Name);
        }

        public Task<string> GetNormalizedRoleNameAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.NormalizedName);
        }

        public Task SetRoleNameAsync(IdentityRole role, string roleName, CancellationToken cancellationToken)
        {
            role.Name = roleName;
            return Task.CompletedTask;
        }

        public Task SetNormalizedRoleNameAsync(IdentityRole role, string normalizedName, CancellationToken cancellationToken)
        {
            role.NormalizedName = normalizedName;
            return Task.CompletedTask;
        }

        public async Task<IdentityResult> UpdateAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            await _roles.ReplaceOneAsync(r => r.Id == role.Id, role, new ReplaceOptions(), cancellationToken);
            return IdentityResult.Success;
        }

        public void Dispose()
        {
            // Không cần dispose gì cả
        }
    }
}
