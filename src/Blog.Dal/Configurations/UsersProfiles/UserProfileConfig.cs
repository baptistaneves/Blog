using Blog.Domain.Aggregates.UserProfileAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Dal.Configurations.UsersProfiles
{
    internal class UserProfileConfig : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.HasKey(up=>up.UserProfileId);

            builder.Property(up => up.IdentityId)
                .HasColumnType("varchar(255)");

            builder.Property(up => up.Role)
               .HasColumnType("varchar(30)");

            builder.OwnsOne(up => up.BasicInfo);

            builder.OwnsOne(up => up.BasicInfo)
                .Property(bi => bi.EmailAddress)
                .HasColumnType("varchar(255)")
                .HasColumnName("EmailAddress");

            builder.OwnsOne(up => up.BasicInfo)
               .Property(bi => bi.FirstName)
               .HasColumnType("varchar(100)")
               .HasColumnName("FirstName");

            builder.OwnsOne(up => up.BasicInfo)
              .Property(bi => bi.LastName)
              .HasColumnType("varchar(100)")
              .HasColumnName("LastName");

            builder.OwnsOne(up => up.BasicInfo)
              .Property(bi => bi.Phone)
              .HasColumnType("varchar(50)")
              .HasColumnName("Phone");

            builder.HasMany(up => up.Posts)
                .WithOne(p => p.UserProfile)
                .HasForeignKey(p => p.UserProfileId);

            builder.HasMany(up => up.PostComments)
                .WithOne(p => p.UserProfile)
                .HasForeignKey(p => p.UserProfileId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(up => up.PostReactions)
                .WithOne(p => p.UserProfile)
                .HasForeignKey(p => p.UserProfileId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(up => up.PostCommentResponses)
                .WithOne(p => p.UserProfile)
                .HasForeignKey(p => p.UserProfileId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(up => up.PostCommentReactions)
                .WithOne(p => p.UserProfile)
                .HasForeignKey(p => p.UserProfileId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("UsersProfiles");
        }
    }
}
