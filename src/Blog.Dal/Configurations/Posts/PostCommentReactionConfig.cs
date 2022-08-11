using Blog.Domain.Aggregates.PostAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Dal.Configurations.Posts
{
    internal class PostCommentReactionConfig : IEntityTypeConfiguration<PostCommentReaction>
    {
        public void Configure(EntityTypeBuilder<PostCommentReaction> builder)
        {
            builder.HasKey(p => p.PostCommentReactionId);

            builder.ToTable("PostCommentReactions");
        }
    }
}
