using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OnlineBlog.Server.Services;
using OnlineBlog.Server.Token;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace OnlineBlog.Server.Controllers
{
    /// <summary>
    /// Токен авторизации
    /// </summary>
    [ApiController]
    [Route("[controller]")] // маршрут до контроллеров 
    public class Token : Controller
    {
        private readonly IdentityService _identityService;

        public Token(IdentityService identityService)
        {
            _identityService = identityService;
        }

        /// <summary>
        /// Получить jwt токен авторизации
        /// </summary>
        [HttpPost]
        public object GetToken()
        {
            // get user data from Db
            var userData = _identityService.GetUserLoginPassFromBasicAuth(Request);

            // get identity
            (ClaimsIdentity claims, int id)? identity = _identityService.GetIdentity(userData.login, userData.password);
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
    }
}
