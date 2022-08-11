using Blog.Domain.Aggregates.PostAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Dal.Configurations.Posts
{
    internal class PostCommentConfig : IEntityTypeConfiguration<PostComment>
    {
        public void Configure(EntityTypeBuilder<PostComment> builder)
        {
            builder.HasKey(p => p.PostCommentId);

            builder.Property(p => p.Text)
                .HasColumnType("varchar(max)");

            builder.HasMany(p => p.Responses)
                .WithOne(c => c.PostComment)
                .HasForeignKey(c => c.PostCommentId);

            builder.HasMany(p => p.Reactions)
               .WithOne(c => c.PostComment)
               .HasForeignKey(c => c.PostCommentId);

            builder.ToTable("PostComments");
        }
    }
}
