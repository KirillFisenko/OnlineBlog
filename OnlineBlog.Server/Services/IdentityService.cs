using OnlineBlog.Server.Data;
using System.Security.Claims;
using System.Text;

namespace OnlineBlog.Server.Services
{
    public class IdentityService
    {
        private readonly UsersService _usersService;

        public IdentityService(UsersService usersService)
        {
            _usersService = usersService;
        }

        public (string login, string password) GetUserLoginPassFromBasicAuth(HttpRequest request)
        {
            string userName = "";
            string userPassword = "";
            string authHeader = request.Headers["Authorization"].ToString();
            if (authHeader != null && authHeader.StartsWith("Basic"))
            {
                string encodedUserNamePassword = authHeader.Replace("Basic ", "");
                Encoding encoding = Encoding.GetEncoding("iso-8859-1");
                string[] namePasswordArray = encoding.GetString(Convert.FromBase64String(encodedUserNamePassword)).Split(':');
                userName = namePasswordArray[0];
                userPassword = namePasswordArray[1];
            }
            return (userName, userPassword);
        }

        public (ClaimsIdentity identity, Guid id)? GetIdentity(string email, string password)
        {
            User currentUser = _usersService.GetUserByEmail(email);
            if (currentUser == null || !VerifyHashedPassword(currentUser.Password, password))
            {
                return null;
            }
            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, currentUser.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, "User")
                };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                claims,
                "Token",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            return (claimsIdentity, currentUser.Id);
        }

        private bool VerifyHashedPassword(string password1, string password2)
        {
            return password1 == password2;
        }
    }
}
