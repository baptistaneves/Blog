using Blog.Domain.Entities.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Dal.Configurations.Categories
{
    internal class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c=> c.CategoryId);

            builder.Property(c => c.Description)
                .HasColumnType("varchar(255)");

            builder.HasMany(c=> c.Posts)
                .WithOne(p => p.Category)
                .HasForeignKey(p=> p.CategoryId);

            builder.ToTable("Categories");
        }
    }
}
