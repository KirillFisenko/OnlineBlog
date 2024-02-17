using OnlineBlog.Server.Data;
using System.Security.Claims;
using System.Text;

namespace OnlineBlog.Server.Services
{
    public class UsersService
    {
        public (string login, string password) GetUserLoginPassFromBasicAuth(HttpRequest request)
        {
            string userName = string.Empty;
            string userPassword = string.Empty;
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
            User currentUser = GetUserByLogin(email);
            
            if (currentUser == null || !VerifyHashedPassword(currentUser.Password, password))
            {
                return null;
            }

            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, currentUser.Email)                    
                };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                claims, 
                "Token", 
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            return (claimsIdentity, currentUser.Id);           
        }

        private User GetUserByLogin(string email)
        {
            throw new NotImplementedException();
        }

        private bool VerifyHashedPassword(string password1, string password2)
        {
            return password1 == password2;
        }
    }
}