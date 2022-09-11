using Blog.Domain.Aggregates.PostAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Dal.Configurations.Posts
{
    internal class CommentReactionConfig : IEntityTypeConfiguration<CommentReaction>
    {
        public void Configure(EntityTypeBuilder<CommentReaction> builder)
        {
            builder.HasKey(p => p.CommentReactionId);

            builder.ToTable("CommentReactions");
        }
    }
}
