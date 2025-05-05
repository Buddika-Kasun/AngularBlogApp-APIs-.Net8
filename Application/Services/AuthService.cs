using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Results;
using Application.Errors;
using Application.Interfaces;
using Application.Models.Request;
using Application.Validators;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class AuthService(
        IUnitOfWork unitOfWork, 
        IUserRepository userRepository, 
        LoginRequestValidator loginRequestValidator, 
        RegisterRequestValidator registerRequestValidator,
        IJwtService jwtService
    ) : IAuthService
    {
        public async Task<Result> LoginAsync(LoginRequest loginRequest)
        {
            var validationResult = await loginRequestValidator.ValidateAsync(loginRequest);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(a => a.ErrorMessage);
                return Result.Failure(AuthError.CreateInvalidRequestError(errors));
            }

            var (email, password) = loginRequest;

            var user = await userRepository.GetUserByEmailAsync(email);

            if (user is null)
            {
                return Result.Failure(AuthError.UserNotFound);
            }

            if (user.Password != password)
            {
                return Result.Failure(AuthError.InvalidPassword);
            }

            var token = await jwtService.GenerateTokenAsync(user);

            var result = new
            {
                Token = token,
                Username = user.Username,
            };

            return Result.Success(result);
        }

        public async Task<Result> RegisterAsync(RegisterRequest registerRequest)
        {
            var validationResult = await registerRequestValidator.ValidateAsync(registerRequest);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(a => a.ErrorMessage);
                return Result.Failure(AuthError.CreateInvalidRequestError(errors));
            }

            var userExist = await userRepository.GetUserByEmailAsync(registerRequest.Email);

            if (userExist is not null)
            {
                return Result.Failure(AuthError.UserAlreadyExist);
            }

            var user = new User
            {
                Username = registerRequest.Username,
                Email = registerRequest.Email,
                Password = registerRequest.Password,
            };

            await userRepository.AddAsync(user);
            await unitOfWork.CommitAsync();

            return Result.Success("User register successfully");
        }
    }
}
