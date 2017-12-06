using Instagraph.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Instagraph.Data.EntityConfig
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(e => e.Id);
            
            builder.Property(e => e.Caption)
                .IsRequired()
                .IsUnicode();

            builder.HasOne(e => e.User)
                .WithMany(s => s.Posts)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Picture)
                .WithMany(s => s.Posts)
                .HasForeignKey(e => e.PictureId);
        }
    }
}
