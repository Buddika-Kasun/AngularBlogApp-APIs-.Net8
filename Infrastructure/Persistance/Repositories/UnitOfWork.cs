using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Infrastructure.Presistance.Context;

namespace Infrastructure.Persistance.Repositories
{
    public class UnitOfWork(BlogDbContext context) : IUnitOfWork
    {
        public void Commit()
        {
            context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
