using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Results;
using Application.DTOs;
using Application.Errors;
using Application.Interfaces;
using Application.Models.Request;
using Application.Validators;
using Domain.Interfaces;

namespace Application.Services
{
    public class UserService(
            UserUpdateRequestValidator userUpdateRequestValidator, 
            IUserRepository userRepository, 
            IUnitOfWork unitOfWork,
            IUserRoleRepository userRoleRepository
        ) : IUserService
    {
        public async Task<Result<PageResult<UserDto>>> GetAsync(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                //var users = await userRepository.GetAllAsync();
                var users = await userRepository.GetAllUsersAsync();
                var totalCount = users.Count();
                var pagedItems = users
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Select(user => new UserDto(
                        user.Id,
                        user.Email,
                        user.Username,
                        user.UserRoles.Select(x => x.Role.Name).ToList()
                     ))
                    .ToList();

                var pageResult = new PageResult<UserDto>
                {
                    Items = pagedItems,
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                return Result.Success(pageResult);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<Result<UserDto>> GetByIdAsync(int id)
        {
            try
            {
                //var user = await userRepository.GetByIdAsync(id);
                var user = await userRepository.GetUserByIdAsync(id);

                if (user is null)
                {
                    return Result.Failure<UserDto>(UserError.UserNotFound);
                }

                UserDto userDetails = new UserDto(
                    user.Id,
                    user.Email,
                    user.Username,
                    user.UserRoles.Select(x => x.Role.Name).ToList()
                 );

                return Result.Success(userDetails);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<Result<string>> UpdateAsync(UserUpdateRequest userUpdateRequest)
        {
            try
            {
                // Validate the request
                var validationRequest = await userUpdateRequestValidator.ValidateAsync(userUpdateRequest);
                if (!validationRequest.IsValid)
                {
                    var errors = validationRequest.Errors.Select(e => e.ErrorMessage);
                    return Result.Failure<string>(UserError.InvalidUserUpdateRequestError(errors));
                }

                // Chech user exists
                var user = await userRepository.GetByIdAsync(userUpdateRequest.Id);
                if (user is null)
                {
                    return Result.Failure<string>(UserError.UserNotFound);
                }

                // Update the user
                user.Username = userUpdateRequest.Username;
                user.Email = userUpdateRequest.Email;

                userRepository.Update(user);
                await unitOfWork.CommitAsync();

                return Result.Success("User update successfully");

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<Result<string>> AssignRoleAsync(RoleRequest roleRequest)
        {
            try
            {
                var isUserHasRole = await userRoleRepository.HasRoleAsync(roleRequest.UserId, roleRequest.RoleId);

                if (isUserHasRole)
                {
                    return Result.Failure<string>(UserError.UserAlreadyHasRole);
                }

                var result = await userRoleRepository.AddAsync(roleRequest.UserId, roleRequest.RoleId);
                if (result)
                {
                    return Result.Success("Role assigned successfully");
                }
                else
                {
                    return Result.Failure<string>(UserError.FailedToAssignRole);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<Result<string>> RevokeRoleAsync(RoleRequest roleRequest)
        {
            try
            {
                var isUserHasRole = await userRoleRepository.HasRoleAsync(roleRequest.UserId, roleRequest.RoleId);

                if (!isUserHasRole)
                {
                    return Result.Failure<string>(UserError.UserHasNotRole);
                }

                var result = await userRoleRepository.RemoveAsync(roleRequest.UserId, roleRequest.RoleId);
                if (result)
                {
                    return Result.Success("Role revoke successfully");
                }
                else
                {
                    return Result.Failure<string>(UserError.FailedToRevokeRole);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<Result<string>> DeleteAsync(int id)
        {
            var user = await userRepository.GetByIdAsync(id);

            if (user is null)
            {
                return Result.Failure<string>(UserError.UserNotFound);
            }

            userRepository.Delete(user);
            await unitOfWork.CommitAsync();

            return Result.Success("User delete successfully");
        }
    }
}
