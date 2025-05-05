using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MrKatsuWebAPI.DataAccess.Entities;

namespace MrKatsuWebAPI.DataAccess.Configurations
{
    public class ModConfiguration : IEntityTypeConfiguration<Mod>
    {
        public void Configure(EntityTypeBuilder<Mod> builder)
        {
            builder.ToTable("mods");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasColumnName("name").IsRequired();
            builder.Property(x => x.Description).HasColumnName("description").IsRequired();
            builder.Property(x => x.CreatedAt).HasColumnName("created_at").IsRequired().HasDefaultValue(DateTime.UtcNow);
            builder.Property(x => x.UpdatedAt).HasColumnName("updated_at").IsRequired().HasDefaultValue(DateTime.UtcNow);
            builder.Property(x => x.IsDeleted).HasColumnName("is_deleted").IsRequired().HasDefaultValue(false);
            builder.Property(x => x.UserId).HasColumnName("user_id");
            builder.Property(x => x.CategoryId).HasColumnName("category_id").IsRequired();
            builder.Property(x => x.IsPrivate).HasColumnName("is_private").IsRequired().HasDefaultValue(false);
            builder.HasOne(x => x.Category).WithMany(x => x.Mods).HasForeignKey(x => x.CategoryId);
            builder.HasOne(x => x.User).WithMany(x => x.Mods).HasForeignKey(x => x.UserId);

        }
    }
}
