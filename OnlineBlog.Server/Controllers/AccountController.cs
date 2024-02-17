using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OnlineBlog.Server.Models;
using OnlineBlog.Server.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace OnlineBlog.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private UsersService _usersService;
        public AccountController()
        {
            _usersService = new UsersService();
        }

        [HttpGet]
        public IActionResult Get()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public ActionResult<UserModel> Create(UserModel user)
        {
            //Create in Db
            return Ok(user);
        }

        [HttpPatch]
        public ActionResult<UserModel> Update(UserModel user)
        {
            //Check user
            //Update in Db
            return Ok(user);
        }

        [HttpDelete]
        public ActionResult<UserModel> Delete(UserModel user)
        {
            //Delete in Db
            return Ok(user);
        }

        [HttpPost]
        public ActionResult<UserModel> GetToken()
        {
            // get user data from Db
            var userData = _usersService.GetUserLoginPassFromBasicAuth(Request);            
            // get identity
            (ClaimsIdentity claims, Guid id)? identity = _usersService.GetIdentity(userData.login, userData.password);
            if(identity == null)
            {
                return NotFound("Логин или пароль не корректен");
            }
            // create jwt token
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity?.claims.Claims,
                expires: now.AddMinutes(AuthOptions.LIFETIME),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            //return token

            var tokenModel = new AuthToken(
                minutes: AuthOptions.LIFETIME,
                accessToken: encodedJwt,
                userName: userData.login,
                userId: identity.Value.id);
            return Ok(tokenModel);
        }
    }
}