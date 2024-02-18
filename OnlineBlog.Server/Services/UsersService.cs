using OnlineBlog.Server.Data;
using OnlineBlog.Server.Models;
using System.Security.Claims;
using System.Text;

namespace OnlineBlog.Server.Services
{
    public class UsersService
    {
        private AppDataContext _dataContext;
        public UsersService(AppDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        #region CRUD
        public User GetUserByLogin(string email)
        {
            return _dataContext.Users.FirstOrDefault(user => user.Email == email);
        }

        public UserModel Create(UserModel userModel)
        {
            var newUser = new User()
            {
                Email = userModel.Email,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Password = userModel.Password,
                Photo = userModel.Photo,
                Description = userModel.Description
            };

            _dataContext.Users.Add(newUser);
            _dataContext.SaveChanges();
            userModel.Id = newUser.Id;
            return userModel;
        }

        public UserModel Update(UserModel userModel)
        {
            var userToUpdate = _dataContext.Users.FirstOrDefault(user => user.Id == userModel.Id);

            if (userToUpdate == null)
            {
                return null;
            }

            userToUpdate.Email = userModel.Email;
            userToUpdate.FirstName = userModel.FirstName;
            userToUpdate.LastName = userModel.LastName;
            userToUpdate.Password = userModel.Password;
            userToUpdate.Description = userModel.Description;

            _dataContext.Users.Update(userToUpdate);
            _dataContext.SaveChanges();
            return userModel;
        }

        public void Delete(User user)
        {
            _dataContext.Users.Remove(user);
            _dataContext.SaveChanges();
        }
        #endregion

        #region Identity
        public (string login, string password) GetUserLoginPassFromBasicAuth(HttpRequest request)
        {
            string userName ="";
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
            User currentUser = GetUserByLogin(email);
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
        #endregion
    }
}