using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Presistance.Context
{
    public class BlogDbContextFactory : IDesignTimeDbContextFactory<BlogDbContext>
    {
        public BlogDbContext CreateDbContext(string[] args)
        {
            var optionBuilder = new DbContextOptionsBuilder<BlogDbContext>();
            optionBuilder.UseSqlServer("Data Source=BIKZ;Initial Catalog=AngularBlogAppDb;Integrated Security=True;Trust Server Certificate=True;");

            return new BlogDbContext(optionBuilder.Options);
        }
    }
}
