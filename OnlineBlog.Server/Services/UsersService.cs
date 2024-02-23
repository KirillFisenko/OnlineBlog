using OnlineBlog.Server.Data;
using OnlineBlog.Server.Helpers;
using OnlineBlog.Server.Models;
using System.Security.Claims;
using System.Text;

namespace OnlineBlog.Server.Services
{
    /// <summary>
    /// Сервис пользователей
    /// </summary>
    public class UsersService
    {
        private AppDataContext _dataContext;
        private NoSQLDataService _noSQLDataService;
        private Mapping _mapping;
        public UsersService(AppDataContext dataContext, NoSQLDataService noSQLDataService, Mapping mapping)
        {
            _dataContext = dataContext;
            _noSQLDataService = noSQLDataService;
            _mapping = mapping;
        }

        #region CRUD
        /// <summary>
        /// Создать пользователя в БД
        /// </summary>
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

        /// <summary>
        /// Получить пользователя по Email из БД
        /// </summary>
        public User GetUserByEmail(string email)
        {
            return _dataContext.Users.FirstOrDefault(user => user.Email == email);
        }

        /// <summary>
        /// Обновить пользователя в БД
        /// </summary>
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
            userToUpdate.Photo = userModel.Photo;

            _dataContext.Users.Update(userToUpdate);
            _dataContext.SaveChanges();
            return userModel;
        }

        /// <summary>
        /// Удалить пользователя из БД
        /// </summary>
        public void Delete(User user)
        {
            _dataContext.Users.Remove(user);
            _dataContext.SaveChanges();
        }
        #endregion

        /// <summary>
        /// Найти всех пользователей по имени
        /// </summary>
        public List<UserModel> GetUsersByName(string name)
        {
            return _dataContext.Users
                .Where(n => n.FirstName.ToLower().Contains(name.ToLower())
                || n.LastName.ToLower().Contains(name.ToLower()))
                .Select(_mapping.ToModel)
                .ToList();
        }



        /// <summary>
        /// Подписаться на пользователя
        /// </summary>
        /// <param name="from">
        /// Id пользователя, который подписывается
        /// </param>
        /// <param name="to">
        /// Id пользователя, на которого подписываются
        /// </param>
        public void Subscribe(Guid from, Guid to)
        {
            _noSQLDataService.SetUserSubs(from, to);
        }

        #region Identity
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
            User currentUser = GetUserByEmail(email);
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