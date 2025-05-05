using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MrKatsuWebAPI.DataAccess.Entities;

namespace MrKatsuWebAPI.DataAccess.Configurations
{
    public class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder.ToTable("roles");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasColumnName("name").IsRequired();
            builder.Property(x => x.NormalizedName).HasColumnName("normalized_name").IsRequired();
            builder.Property(x => x.Description).HasColumnName("description").IsRequired(false);
            builder.Property(x => x.ConcurrencyStamp).HasColumnName("concurrency_stamp").IsRequired(false);
            builder.HasIndex(x => x.Name).IsUnique().HasDatabaseName("IX_Roles_Name");
            builder.HasIndex(x => x.NormalizedName).HasDatabaseName("IX_Roles_NormalizedName");
            builder.HasIndex(x => x.ConcurrencyStamp).HasDatabaseName("IX_Roles_ConcurrencyStamp");

        }
    }
}
