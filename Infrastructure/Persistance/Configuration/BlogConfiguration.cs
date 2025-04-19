using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Presistance.Configuration
{
    public class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.ToTable("Blogs");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("nvarchar(100)");

            builder.Property(b => b.Content)
                .IsRequired();

            builder.Property(b => b.Image)
                .IsRequired();

            builder.Property(b => b.CreatedAt)
                .IsRequired()
                //.HasDefaultValue(DateTime.Now)
                .HasDefaultValueSql("GETDATE()");

            builder.Property(b => b.UpdatedAt)
                .IsRequired()
                //.HasDefaultValue(DateTime.Now)
                .HasDefaultValueSql("GETDATE()");

            builder.HasOne(b => b.User)
                .WithMany(u => u.Blogs)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(b => b.Comments)
                .WithOne(c => c.Blog)
                .HasForeignKey(c => c.BlogId);
        }
    }
}
