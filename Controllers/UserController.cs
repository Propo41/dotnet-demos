using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using DebugDemo.Models;
using DebugDemo.Services;
using Microsoft.AspNetCore.Authorization;
using dotnet_web_api_demo.Services;
using dotnet_web_api_demo.Models;

namespace dotnet_web_api_demo.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly UserService userService;
        public UserController(UserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public ActionResult<List<User>> GetUsers()
        {
            return userService.GetUsers();
        }

        [HttpGet("{id:length(24)}")]
        public ActionResult<List<User>> GetUser(string id)
        {
            var user = userService.GetUser(id);
            return Json(user); // try removing Json and see what happens
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("create-user")]
        public ActionResult<User> Create(User user)
        {
            userService.Create(user);
            return Json(user); // try removing Json and see what happens
        }

        [AllowAnonymous]
        [Route("authenticate")]
        [HttpPost]
        public ActionResult Login(User user)
        {
            var token = userService.Authenticate(user.Email, user.Password);
            if (token == null)
            {
                return Unauthorized();
            }
            // find the user from database, and get the id
            return Ok(new { token, user });

        }



    }
}