using Blog.Domain.Aggregates.PostAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Dal.Configurations.Posts
{
    internal class PostConfig : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(p=> p.PostId);

            builder.Property(p => p.Title)
                .HasColumnType("varchar(255)");

            builder.Property(p => p.Content)
               .HasColumnType("nvarchar(max)");

            builder.Property(p => p.Image)
               .HasColumnType("varchar(255)");

            builder.HasMany(p => p.Comments)
                .WithOne(c => c.Post)
                .HasForeignKey(c => c.PostId);

            builder.HasMany(p => p.Reactions)
               .WithOne(c => c.Post)
               .HasForeignKey(c => c.PostId);

            builder.ToTable("Posts");
        }
    }
}
