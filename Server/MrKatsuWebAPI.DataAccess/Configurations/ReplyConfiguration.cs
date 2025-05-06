using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MrKatsuWebAPI.DataAccess.Entities;

namespace MrKatsuWebAPI.DataAccess.Configurations
{
    public class ReplyConfiguration : IEntityTypeConfiguration<Reply>
    {
        public void Configure(EntityTypeBuilder<Reply> builder)
        {
            builder.ToTable("replies");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();
            builder.Property(x => x.Content).HasColumnName("content").IsRequired();
            builder.Property(x => x.CreatedAt).HasColumnName("created_at").IsRequired().HasDefaultValue(DateTime.UtcNow);
            builder.Property(x => x.UpdatedAt).HasColumnName("updated_at").IsRequired().HasDefaultValue(DateTime.UtcNow);
            builder.Property(x => x.UserId).HasColumnName("user_id");
            builder.Property(x => x.PostId).HasColumnName("post_id");
            builder.Property(x => x.ParentId).HasColumnName("parent_id").IsRequired(false);
            builder.Property(x => x.IsDeleted).HasColumnName("is_deleted").IsRequired().HasDefaultValue(false);
            builder.HasOne(x => x.User).WithMany(x => x.Replies).HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.Post).WithMany(x => x.Replies).HasForeignKey(x => x.PostId);
            builder.HasOne(x => x.Parent).WithMany(x => x.Children).HasForeignKey(x => x.ParentId);
            
            builder.HasIndex(x => x.UpdatedAt).HasDatabaseName("IX_Replies_UpdatedAt");
            builder.HasIndex(x => x.UserId).HasDatabaseName("IX_Replies_UserId");
            builder.HasIndex(x => x.PostId).HasDatabaseName("IX_Replies_PostId");
            builder.HasIndex(x => x.IsDeleted).HasDatabaseName("IX_Replies_IsDeleted");

        }
    }
}
