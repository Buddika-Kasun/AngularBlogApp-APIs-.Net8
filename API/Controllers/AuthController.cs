using Application.Interfaces;
using Application.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IResult> Register(RegisterRequest registerRequest)
        {
            var response = await authService.RegisterAsync(registerRequest);

            return Results.Ok(response);
        }

        [HttpPost("login")]
        public async Task<IResult> Login(LoginRequest loginRequest)
        {

            return Results.Ok(loginRequest);
        }
    }
}
