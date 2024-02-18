using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OnlineBlog.Server.Data;
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
        public AccountController(AppDataContext dataContext)
        {
            _usersService = new UsersService(dataContext);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var currentUserEmail = HttpContext.User.Identity.Name;
            var currentUser = _usersService.GetUserByLogin(currentUserEmail);
            if (currentUser == null)
            {
                return BadRequest();
            }

            return Ok(new UserModel()
            {
                Id = currentUser.Id,
                Email = currentUser.Email,
                FirstName = currentUser.FirstName,
                LastName = currentUser.LastName,
                Description = currentUser.Description,
                Photo = currentUser.Photo
            });
        }

        [HttpPost]
        public ActionResult<UserModel> Create(UserModel user)
        {
            var newuUser = _usersService.Create(user);
            return Ok(newuUser);
        }

        [HttpPatch]
        public ActionResult<UserModel> Update(UserModel user)
        {
            var currentUserEmail = HttpContext.User.Identity.Name;
            var currentUser = _usersService.GetUserByLogin(currentUserEmail);
            if (currentUser == null)
            {
                return BadRequest();
            }
            _usersService.Update(user);
            return Ok(user);
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            var currentUserEmail = HttpContext.User.Identity.Name;
            var currentUser = _usersService.GetUserByLogin(currentUserEmail);
            _usersService.Delete(currentUser);
            return Ok();
        }

        [HttpPost]
        public ActionResult<UserModel> GetToken()
        {
            // get user data from Db
            var userData = _usersService.GetUserLoginPassFromBasicAuth(Request);

            // get identity
            (ClaimsIdentity claims, Guid id)? identity = _usersService.GetIdentity(userData.login, userData.password);
            if (identity == null)
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