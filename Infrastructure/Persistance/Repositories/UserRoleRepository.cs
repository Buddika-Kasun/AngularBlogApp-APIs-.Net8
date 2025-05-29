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
    public class UserRoleRepository(BlogDbContext context) : IUserRoleRepository
    {
        public async Task<bool> AddAsync(int userId, int roleId)
        {
            try
            {
                var userRole = new UserRole
                { 
                    UserId = userId,
                    RoleId = roleId
                };

                await context.UserRoles.AddAsync(userRole);
                var result = await context.SaveChangesAsync() > 0;
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<bool> RemoveAsync(int userId, int roleId)
        {
            try
            {
                var userRole = await context.UserRoles.FirstOrDefaultAsync(x => x.UserId == userId && x.RoleId == roleId);
                if (userRole is null)
                {
                    return false;
                }

                context.UserRoles.Remove(userRole);
                var result = await context.SaveChangesAsync() > 0;
                return result;

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<bool> HasRoleAsync(int userId, int roleId)
        {
            try
            {
                var isUserHasRole = await context.UserRoles.AnyAsync(x => x.UserId == userId && x.RoleId == roleId);

                return isUserHasRole;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
