using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MrKatsuWebAPI.DataAccess.Entities;

namespace MrKatsuWebAPI.DataAccess.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("users");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();
            builder.Property(x => x.UserName).HasColumnName("username").IsRequired();
            builder.Property(x => x.Email).HasColumnName("email").IsRequired();
            builder.Property(x => x.PasswordHash).HasColumnName("password_hash").IsRequired();
            builder.Property(x => x.IsDeleted).HasColumnName("is_deleted").IsRequired().HasDefaultValue(false);
            builder.Property(x => x.NormalizedEmail).HasColumnName("normalized_email");
            builder.Property(x => x.NormalizedUserName).HasColumnName("normalized_username");
            builder.Property(x => x.TimeCreated).HasColumnName("time_created");
            builder.Property(x => x.TimeUpdated).HasColumnName("time_updated");
            builder.Property(x => x.Avatar).HasColumnName("avatar").HasDefaultValue(string.Empty);
            builder.Property(x => x.SecurityStamp).HasColumnName("security_stamp").HasDefaultValue(string.Empty);
            builder.Property(x => x.ConcurrencyStamp).HasColumnName("concurrency_stamp").HasDefaultValue(string.Empty);
            builder.Property(x => x.PhoneNumber).HasColumnName("phone_number").HasDefaultValue(string.Empty);
            builder.Property(x => x.PhoneNumberConfirmed).HasColumnName("phone_number_confirmed").HasDefaultValue(false);
            builder.Property(x => x.TwoFactorEnabled).HasColumnName("two_factor_enabled").HasDefaultValue(false);
            builder.Property(x => x.LockoutEnabled).HasColumnName("lockout_enabled").HasDefaultValue(false);
            builder.Property(x => x.LockoutEnd).HasColumnName("lockout_end").IsRequired(false).HasDefaultValue(null);
            builder.Property(x => x.AccessFailedCount).HasColumnName("access_failed_count").HasDefaultValue(0);
            builder.Property(x => x.EmailConfirmed).HasColumnName("email_confirmed").HasDefaultValue(false);


            builder.HasMany(x => x.Posts).WithOne(x => x.User).HasForeignKey(x => x.UserId);
            builder.HasMany(x => x.Replies).WithOne(x => x.User).HasForeignKey(x => x.UserId);
            builder.HasIndex(x => x.UserName).IsUnique().HasDatabaseName("IX_Users_UserName");
        }
    }
}
