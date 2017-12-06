using Instagraph.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Instagraph.Data.EntityConfig
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasAlternateKey(e => e.Username);

            builder.Property(e => e.Username)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(30);

            builder.Property(e => e.Password)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(20);

            builder.HasOne(e => e.ProfilePicture)
                .WithMany(s => s.Users)
                .HasForeignKey(e => e.ProfilePictureId);
        }
    }
}
