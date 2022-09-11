using Blog.Domain.Aggregates.PostAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Dal.Configurations.Posts
{
    internal class CommentAnswerConfig : IEntityTypeConfiguration<CommentAnswer>
    {
        public void Configure(EntityTypeBuilder<CommentAnswer> builder)
        {
            builder.HasKey(p => p.CommentAnswerId);

            builder.Property(p => p.Text)
                .HasColumnType("varchar(max)");

            builder.ToTable("CommentAnswers");
        }
    }
}
