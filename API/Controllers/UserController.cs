﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        [Authorize]
        [HttpGet]
        public string[] GetUsers()
        {
            return [ "User 1", "user 2", "user 3"];
        }

        // Create API endpoint for user
        //      - GetUsers
        //      - GetUserById
        //      - UpdateUser
        //      - DeleteUser
        //      - AssignAdminRole
        //      - RevokeAdminRole
    }
}
