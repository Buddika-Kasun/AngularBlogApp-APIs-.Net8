using API.Extensions;
using Application.Interfaces;
using Application.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : Controller
    {
        // Create API endpoint for user

        //      - GetAllUsers
        //[Authorize]
        [HttpGet]
        public async Task<IResult> GetAllUsers(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10
        )
        {
            var result = await userService.GetAsync(pageNumber, pageSize);

            return result.ToHttpResponse();
        }

        //      - GetUserById
        [HttpGet("{id}")]
        public async Task<IResult> GetUserById(int id)
        {
            var result = await userService.GetByIdAsync(id);

            return result.ToHttpResponse();
        }

        //      - UpdateUser
        [HttpPut]
        public async Task<IResult> UpdateUser([FromBody] UserUpdateRequest userUpdateRequest)
        {
            var result = await userService.UpdateAsync(userUpdateRequest);

            return result.ToHttpResponse();
        }

        //      - DeleteUser
        [HttpDelete("{id}")]
        public async Task<IResult> DeleteUser(int id)
        {
            var result = await userService.DeleteAsync(id);

            return result.ToHttpResponse();
        }

        //      - AssignAdminRole
        [HttpPost("assign-role")]
        public async Task<IResult> AssignRole([FromBody] RoleRequest roleRequest)
        {
            var result = await userService.AssignRoleAsync(roleRequest);

            return result.ToHttpResponse();
        }

        //      - RevokeAdminRole
        [HttpDelete("revoke-role")]
        public async Task<IResult> RevokeRole([FromBody] RoleRequest roleRequest)
        {
            var result = await userService.RevokeRoleAsync(roleRequest);

            return result.ToHttpResponse();
        }
    }
}
