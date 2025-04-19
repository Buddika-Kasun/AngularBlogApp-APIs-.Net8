using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Models.Request;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class AuthService(IUnitOfWork unitOfWork, IUserRepository userRepository) : IAuthService
    {
        public Task<string> LoginAsync(LoginRequest loginRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<string> RegisterAsync(RegisterRequest registerRequest)
        {
            if (registerRequest == null)
            {
                throw new ArgumentNullException(nameof(registerRequest));
            }

            var userExist = await userRepository.GetUserByEmailAsync(registerRequest.Email);

            if (userExist is not null)
            {
                throw new Exception("User already exist!");
            }

            var user = new User
            {
                Username = registerRequest.Username,
                Email = registerRequest.Email,
                Password = registerRequest.Password,
            };

            await userRepository.AddAsync(user);
            await unitOfWork.CommitAsync();

            return "Register Success!";
        }
    }
}
