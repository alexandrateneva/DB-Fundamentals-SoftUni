using Instagraph.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Instagraph.Data.EntityConfig
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Content)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(250);

            builder.HasOne(e => e.User)
                .WithMany(s => s.Comments)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict); 

            builder.HasOne(e => e.Post)
                .WithMany(s => s.Comments)
                .HasForeignKey(e => e.PostId);
        }
    }
}
