using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using NSI.DC.Auth;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NSI.REST.Middleware
{
    public class AuthHandler
    {
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly RequestDelegate _next;

        public AuthHandler(RequestDelegate next, IOptions<JwtIssuerOptions> jwtOptions)
        {
            _next = next;
            _jwtOptions = jwtOptions.Value;
        }

        public Task Invoke(HttpContext context)
        {
            if (context.Request.Path.HasValue && context.Request.Path.Value.ToLower() == "/api/authorization/checktoken")
            {
                var token = context.Request.Headers[_jwtOptions.TokenName];
                if (string.IsNullOrWhiteSpace(token))
                    return null;

                var validationParamters = new TokenValidationParameters
                {
                    ValidIssuer = _jwtOptions.Issuer,
                    ValidAudience = _jwtOptions.Audience,
                    IssuerSigningKey = _jwtOptions.SigningCredentials.Key,
                    RequireExpirationTime = true
                };

                try
                {
                    context.User = new JwtSecurityTokenHandler().ValidateToken(token, validationParamters, out SecurityToken jwt);
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = 401;
                    return context.Response.WriteAsync("");
                }
            }
            else if (context.Request.Path.HasValue && context.Request.Path.Value.ToLower() != "/api/authorization/login")
            {
                var token = context.Request.Headers[_jwtOptions.TokenName];
                if (string.IsNullOrWhiteSpace(token))
                    return null;

                var validationParamters = new TokenValidationParameters
                {
                    ValidIssuer = _jwtOptions.Issuer,
                    ValidAudience = _jwtOptions.Audience,
                    IssuerSigningKey = _jwtOptions.SigningCredentials.Key,
                    RequireExpirationTime = true
                };

                try
                {
                    context.User = new JwtSecurityTokenHandler().ValidateToken(token, validationParamters, out SecurityToken jwt);
                    var branchIDStr = context.Request.Headers["SelectedBranch"];
                    if (string.IsNullOrWhiteSpace(branchIDStr) || !Int32.TryParse(branchIDStr, out int branchID))
                    {
                        context.Response.StatusCode = 402;
                        return context.Response.WriteAsync("");
                    }

                    var userDetails = JsonConvert.DeserializeObject<User>(context.User.FindFirst("userDetails").Value);

                    ((ClaimsIdentity)context.User.Identity).AddClaim(new Claim("CompanyID", userDetails.Company.ID.ToString()));
                    ((ClaimsIdentity)context.User.Identity).AddClaim(new Claim("BranchID", branchID.ToString()));

                    context.Items.Add("User", userDetails);
                    context.Items.Add("CompanyID", userDetails.Company.ID);
                    context.Items.Add("BranchID", branchID);

                    if (jwt.ValidTo - DateTime.UtcNow < new TimeSpan(_jwtOptions.ValidFor.Ticks / 2))
                    {
                        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
                        context.Response.Headers.Add(_jwtOptions.TokenName, encodedJwt);
                    }
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = 401;
                    return context.Response.WriteAsync("");
                }
            }

            return _next(context);
        }
    }
}
