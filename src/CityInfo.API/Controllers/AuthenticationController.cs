using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CityInfo.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public class AuthenticationRequestBody
        {
            public string? UserName { get; set; }
            public string? Password { get; set; }
        }


        public AuthenticationController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


        [HttpPost]        
        public ActionResult<string> Authenticate(AuthenticationRequestBody authenticationRequestBody)
        {
             var logFile = Path.Combine(AppContext.BaseDirectory, "logs/auth.txt");
            var user = ValidateCredentials(
                authenticationRequestBody.UserName,
                authenticationRequestBody.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            // write to a text file
           
            var secretValue = configuration["Authentication:Secret"] ?? "not found";

            System.IO.File.AppendAllText(logFile, $"Configration secret: {secretValue}\n");



            // Create a token
            var securityKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(configuration["Authentication:Secret"]!));
            var signingCredentials = new SigningCredentials(securityKey,
                SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>
            {
                new Claim("sub", user.UserId.ToString()),
                new Claim("given_name", user.FirstName),
                new Claim("family_name", user.Surname),
                new Claim("city", user.City.ToString())
            };

            var jwtSecurityToken = new JwtSecurityToken(

                configuration["Authentication:Issuer"],
                 configuration["Authentication:Audience"],
                 claimsForToken,
                 DateTime.Now,
                 DateTime.Now.AddHours(1),
                 signingCredentials);

            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return Ok(tokenToReturn);
        }

        public class CityUser
        {
            public int UserId { get; set; }
            public string? UserName { get; set; }
            public string FirstName { get; set; } = string.Empty;
            public string Surname { get; set; } = string.Empty;
            public string City { get; set; }
        }
        private CityUser ValidateCredentials(string? userName, string? password)
        {
            return new CityUser
            {
                UserId = 1,
                UserName = userName ?? "",
                FirstName = "Jack",
                Surname = "Jones",
                City = "London"
            };
        }
    }
}
