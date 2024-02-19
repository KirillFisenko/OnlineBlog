using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OnlineBlog.Server.Models;
using OnlineBlog.Server.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace OnlineBlog.Server.Controllers
{
    /// <summary>
    /// Контроллер аккаунтов (создать/удалить/изменить/обновить пользователя)
    /// </summary>
    [ApiController]
    [Authorize] // доступ только авторизованным
    [Route("[controller]")] // маршрут до контроллеров   
    public class AccountController : ControllerBase
    {
        private UsersService _usersService;
        public AccountController(UsersService usersService)
        {
            _usersService = usersService;
        }

        #region CRUD
        /// <summary>
        /// Получить пользователя
        /// </summary>
        [HttpGet]
        public IActionResult Get()
        {
            var currentUserEmail = HttpContext.User.Identity.Name;
            var currentUser = _usersService.GetUserByEmail(currentUserEmail);
            return currentUser == null
                ? NotFound()
                : Ok(new UserModel()
                {
                    Id = currentUser.Id,
                    Email = currentUser.Email,
                    FirstName = currentUser.FirstName,
                    LastName = currentUser.LastName,
                    Description = currentUser.Description,
                    Photo = currentUser.Photo
                });
        }

        /// <summary>
        /// Создать пользователя
        /// </summary>
        [HttpPost]
        public ActionResult<UserModel> Create(UserModel user)
        {
            var newUser = _usersService.Create(user);
            return Ok(newUser);
        }

        /// <summary>
        /// Обновить пользователя
        /// </summary>
        [HttpPatch]
        public ActionResult<UserModel> Update(UserModel user)
        {
            var currentUserEmail = HttpContext.User.Identity.Name;
            var currentUser = _usersService.GetUserByEmail(currentUserEmail);
            if (currentUser == null)
            {
                return NotFound();
            }
            _usersService.Update(user);
            return Ok(user);
        }

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        [HttpDelete]
        public IActionResult Delete()
        {
            var currentUserEmail = HttpContext.User.Identity.Name;
            var currentUser = _usersService.GetUserByEmail(currentUserEmail);
            if (currentUser == null)
            {
                return NotFound();
            }
            _usersService.Delete(currentUser);
            return Ok();
        }
        #endregion

        #region Token
        /// <summary>
        /// Получить jwt токен
        /// </summary>
        [HttpPost("token")]
        [AllowAnonymous] // авторизация не нужна
        public object GetToken()
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

            // return token
            var tokenModel = new AuthToken(
                minutes: AuthOptions.LIFETIME,
                accessToken: encodedJwt,
                userName: userData.login,
                userId: identity.Value.id);
            return Ok(tokenModel);
        }
        #endregion
    }
}