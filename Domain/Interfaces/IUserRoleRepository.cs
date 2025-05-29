using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserRoleRepository
    {
        Task<bool> AddAsync(int userId, int roleId);
        Task<bool> RemoveAsync(int userId, int roleId);
        Task<bool> HasRoleAsync(int userId, int roleId);
    }
}
