using Blog.Domain.Aggregates.PostAggregate;
using Blog.Domain.Aggregates.UserProfileAggregate;
using Blog.Domain.Entities.Categories;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog.Dal.Context
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}

        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostComment> PostComments { get; set; }
        public DbSet<PostReaction> PostReactions { get; set; }
        public DbSet<CommentAnswer> CommentAnswers { get; set; }
        public DbSet<CommentReaction> CommentReactions { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
        }
    }
}
