using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Presistance.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.Repositories
{
    internal class UserRepository(BlogDbContext context) : GenaricRepository<User>(context), IUserRepository
    {
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<List<string>> GetUserRoleByEmailAsync(string email)
        {
            return await context.Users
                .Where(u => u.Email == email)
                .SelectMany(u => u.UserRoles)
                .Select(r => r.Role.Name)
                .ToListAsync();
        }
    }
}
