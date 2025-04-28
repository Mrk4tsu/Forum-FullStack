using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MrKatsuWebAPI.DataAccess.Entities;

namespace MrKatsuWebAPI.DataAccess.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("posts");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();
            builder.Property(x => x.Title).HasColumnName("title").IsRequired();
            builder.Property(x => x.Content).HasColumnName("content").IsRequired();
            builder.Property(x => x.CreatedAt).HasColumnName("created_at").IsRequired().HasDefaultValue(DateTime.UtcNow);
            builder.Property(x => x.UpdatedAt).HasColumnName("updated_at").IsRequired().HasDefaultValue(DateTime.UtcNow);
            builder.Property(x => x.UserId).HasColumnName("user_id");
            builder.Property(x => x.IsDeleted).HasColumnName("is_deleted").IsRequired().HasDefaultValue(false);
            builder.HasOne(x => x.User).WithMany(x => x.Posts).HasForeignKey(x => x.UserId);
            builder.HasMany(x => x.Replies).WithOne(x => x.Post).HasForeignKey(x => x.PostId);

            builder.HasIndex(x => x.UpdatedAt).HasDatabaseName("IX_Posts_UpdatedAt");
            builder.HasIndex(x => x.UserId).HasDatabaseName("IX_Posts_UserId");
            builder.HasIndex(x => x.IsDeleted).HasDatabaseName("IX_Posts_IsDeleted");
        }
    }
}
