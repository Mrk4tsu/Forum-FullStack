using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MrKatsuWebAPI.DataAccess.Entities;

namespace MrKatsuWebAPI.DataAccess.Configurations
{
    public class ReactionConfiguration : IEntityTypeConfiguration<Reaction>
    {
        public void Configure(EntityTypeBuilder<Reaction> builder)
        {
            builder.ToTable("reactions");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();
            builder.Property(x => x.Content).HasColumnName("content").IsRequired();
            builder.Property(x => x.CreatedAt).HasColumnName("created_at").IsRequired().HasDefaultValue(DateTime.UtcNow);
            builder.Property(x => x.UpdatedAt).HasColumnName("updated_at").IsRequired().HasDefaultValue(DateTime.UtcNow);
            builder.Property(x => x.IsDeleted).HasColumnName("is_deleted").IsRequired().HasDefaultValue(false);
            builder.Property(x => x.ModId).HasColumnName("mod_id");
            builder.Property(x => x.UserId).HasColumnName("user_id");
            builder.HasOne(x => x.Mod).WithMany(x => x.Reactions).HasForeignKey(x => x.ModId);
            builder.HasOne(x => x.User).WithMany(x => x.Reactions).HasForeignKey(x => x.UserId);
            builder.HasIndex(x => x.UpdatedAt).HasDatabaseName("IX_Reactions_UpdatedAt");
            builder.HasIndex(x => x.UserId).HasDatabaseName("IX_Reactions_UserId");

        }
    }
}
