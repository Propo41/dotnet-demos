using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using dotnet_web_api_demo.Models;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


/* Note that here we are hardcoding collection name etc.
Like I mentioned, in real life you’d be getting these from appsettings.json file. */
namespace dotnet_web_api_demo.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> users;
        private readonly string key;
        private readonly int tokenExpiryTime;
        public UserService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("HyphenDb"));
            var db = client.GetDatabase("HyphenDb");
            users = db.GetCollection<User>("Users");
            key = config["JWT:Secret"];
            tokenExpiryTime = Int32.Parse(config["JWT:ExpiresIn"]);
        }

        /*  
        This method will take email and password passed from 
        login form or in our case request body, and check if credentials 
        are valid and if so, it will create the token with data we want inside it. 
        Method looks like this: 
        */
        public string Authenticate(string email, string password)
        {
            var user = users.Find(u => u.Email == email && u.Password == password).FirstOrDefault();
            if (user == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.Email, email),
                }),
                Expires = DateTime.UtcNow.AddMinutes(tokenExpiryTime),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public List<User> GetUsers() => users.Find(user => true).ToList();

        public User GetUser(string id) => users.Find<User>(user => user.Id == id).FirstOrDefault();

        public User Create(User user)
        {
            users.InsertOne(user);
            return user;
        }
    }
}