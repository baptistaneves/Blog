using Blog.Domain.Aggregates.PostAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Dal.Configurations.Posts
{
    internal class PostCommentResponseConfig : IEntityTypeConfiguration<PostCommentResponse>
    {
        public void Configure(EntityTypeBuilder<PostCommentResponse> builder)
        {
            builder.HasKey(p => p.PostCommentResponseId);

            builder.Property(p => p.Text)
                .HasColumnType("varchar(max)");

            builder.ToTable("PostCommentResponses");
        }
    }
}
