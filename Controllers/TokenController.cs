using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jwt_token.Models;
using jwt_token.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;

namespace jwt_token.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TokenController : ControllerBase
    {

        public TokenController()
        {

        }

        // GET /token
        [HttpGet]
        public ActionResult GetToken()
        {
            Util util = new Util();
            var token = util.GenerateToken(new Models.User() { Id = "1", Email = "userA@gmail.com" });
            //Console.WriteLine(token);
            return Ok(new { token = token });
        }


        // GET /token/verify/{token}
        [HttpGet("verify/{token}")]
        public ActionResult ValidateToken(string token)
        {
            Util util = new Util();
            var res = util.ValidateToken(token);
            //Console.WriteLine(res);
            return Ok(new { validation = res });
        }

        // GET /token/private
        [Authorize]
        [HttpGet("private")]
        public ActionResult FetchRandomNumber(string token)
        {
            Console.WriteLine("Accessing private endpoint");
            // there are several ways to access the context:
            // https://stackoverflow.com/questions/38571032/how-to-get-httpcontext-current-in-asp-net-core 
            HttpContext context = HttpContext;
            User authUser = (User)context.Items["User"];
            Console.WriteLine(authUser.ToJson());

            return Ok(new { randomNumber = new Random().Next(1, 100) });
        }
    }
}
