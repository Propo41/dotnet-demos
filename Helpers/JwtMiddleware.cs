using jwt_token.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace jwt_token.Helpers
{
    public class JwtMiddleware
    {

        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /*
         * Since we assigned JwtMiddleware as our custom middleware in the Startup.cs file ie: app.UseMiddleware<JwtMiddleware>();,
         * this  method gets called implicitly
         * to know more about middlewares: 
         * https://www.c-sharpcorner.com/article/jwt-json-web-token-authentication-in-asp-net-core/ 
         * https://www.c-sharpcorner.com/article/asp-net-core-web-api-5-0-authentication-using-jwtjson-base-token/
        */
        public async Task Invoke(HttpContext context)
        {
            Util util = new Util();
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            Console.WriteLine("Middleware triggered");
            var userId = util.ValidateToken(token);
            if (userId != null)
            {
                // attach user to context on successful jwt validation
                // we can the access this context object in our controller classes

                // im hardcoding the values here, but in a real project this should be coming from a database
                // similar to:  context.Items["User"] = userService.GetById(userId.Value);
                context.Items["User"] = new User(){Id = "69", Email="userFound@gmail.com"};
            }

            await _next(context);
        }
    }
}