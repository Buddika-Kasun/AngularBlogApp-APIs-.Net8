using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Results;
using Application.DTOs;
using Application.Models.Request;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<Result<PageResult<UserDto>>> GetAsync(int pageNumber = 1, int pageSize = 10);
        Task<Result<string>> UpdateAsync(UserUpdateRequest userUpdateRequest);
        Task<Result<string>> DeleteAsync(int id);
        Task<Result<UserDto>> GetByIdAsync(int id);
        Task<Result<string>> AssignRoleAsync(RoleRequest roleRequest);
        Task<Result<string>> RevokeRoleAsync(RoleRequest roleRequest);
    }
}
