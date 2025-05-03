using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Results;
using Application.Models.Request;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<Result> RegisterAsync(RegisterRequest registerRequest);
        Task<Result> LoginAsync(LoginRequest loginRequest);
    }
}
