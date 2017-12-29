using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSI.REST.Middleware;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using NSI.DC.Auth;
using System.IdentityModel.Tokens.Jwt;

namespace NSI.REST.Controllers
{
    [Produces("application/json")]
    [Route("api/Authorization")]
    public class AuthorizationController : Controller
    {
        private readonly JwtIssuerOptions _jwtOptions;        
        private readonly JsonSerializerSettings _serializerSettings;

        //private readonly BLL.Interfaces.IUserManagement _usersMng;

        public AuthorizationController(IOptions<JwtIssuerOptions> jwtOptions/*, BLL.Interfaces.IUserManagement usersMng*/)
        {
            //_usersMng = usersMng;

            _jwtOptions = jwtOptions.Value;
            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
        }

        [Route("login")]
        [HttpGet]
        public IActionResult Login(string username, string passCode)
        {
            //var user = _usersMng.GetUser(username, passCode);
            var user = new User { Id = 10, FirstName = "A", LastName = "B", Email = "a@b.com", Username = "ab", Company = new Company { ID = 100 } };
            if (user == null)
                return new UnauthorizedResult();
            
            // Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: GetClaims(user),
                notBefore: _jwtOptions.NotBefore,
                expires: _jwtOptions.Expiration,
                signingCredentials: _jwtOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                accessToken = encodedJwt
            };

            return new OkObjectResult(response);
        }

        [Route("checkToken")]
        [HttpGet]
        public IActionResult CheckToken()
        {
            return Ok("OK");
        }

        private Claim[] GetClaims(User user)
        {
            return new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Sid, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, _jwtOptions.JtiGenerator().Result),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
                new Claim("userID", user.Id.ToString()),
                new Claim("userDetails", JsonConvert.SerializeObject(user))
            };
        }

        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private static long ToUnixEpochDate(DateTime date)
        {
            return (long)Math.Round((date.ToUniversalTime() -
                               new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                              .TotalSeconds);
        }
    }
}