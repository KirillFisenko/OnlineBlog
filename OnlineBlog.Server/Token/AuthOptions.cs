using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace OnlineBlog.Server.Token
{
    /// <summary>
    /// Настройки Jwt токена
    /// </summary>
    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer"; // издатель токена
        public const string AUDIENCE = "MyAuthClient"; // потребитель токена
        private const string KEY = "mysupersecret_secretkey!123$mysupersecret_secretkey!123$";  // ключ для шифрации
        public const int LIFETIME = 10;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
