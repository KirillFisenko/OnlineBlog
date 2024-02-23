namespace OnlineBlog.Server.Models
{
    /// <summary>
    /// Jwt токен авторизации пользователя
    /// </summary>
    public class AuthToken
    {
        public int Minutes { get; private set; }
        public string AccessToken { get; private set; }
        public string UserName { get; private set; }
        public Guid UserId { get; private set; }

        public AuthToken(int minutes, string accessToken, string userName, Guid userId)
        {
            Minutes = minutes;
            AccessToken = accessToken;
            UserName = userName;
            UserId = userId;
        }
    }
}