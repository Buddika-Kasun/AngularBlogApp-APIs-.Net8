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
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Content)
                .IsRequired();

            builder.Property(c => c.CreatedAt)
                .IsRequired()
                //.HasDefaultValue(DateTime.Now)
                .HasDefaultValueSql("GETDATE()");

            builder.Property(c => c.UpdatedAt)
                .IsRequired()
                //.HasDefaultValue(DateTime.Now)
                .HasDefaultValueSql("GETDATE()");

            builder.HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(c => c.Blog)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.BlogId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
