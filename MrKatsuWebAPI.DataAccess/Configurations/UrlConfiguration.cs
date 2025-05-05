using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MrKatsuWebAPI.DataAccess.Entities;

namespace MrKatsuWebAPI.DataAccess.Configurations
{
    public class UrlConfiguration : IEntityTypeConfiguration<Url>
    {
        public void Configure(EntityTypeBuilder<Url> builder)
        {
            builder.ToTable("urls");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();
            builder.Property(x => x.UrlString).HasColumnName("url_string").IsRequired();
            builder.Property(x => x.CreatedAt).HasColumnName("created_at").HasDefaultValue(DateTime.UtcNow);
            builder.Property(x => x.UpdatedAt).HasColumnName("updated_at").HasDefaultValue(DateTime.UtcNow);
            builder.Property(x => x.IsDeleted).HasColumnName("is_deleted").IsRequired().HasDefaultValue(false);
            builder.Property(x => x.ModId).HasColumnName("mod_id");
            builder.HasOne(x => x.Mod).WithMany(x => x.Urls).HasForeignKey(x => x.ModId);
        }
    }
}
