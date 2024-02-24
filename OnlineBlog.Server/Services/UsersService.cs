using OnlineBlog.Server.Data;
using OnlineBlog.Server.Helpers;
using OnlineBlog.Server.Models;

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
        /// Получить пользователя по Id из БД
        /// </summary>
        public UserProfileModel GetUserProfileById(Guid userId)
        {
            return _mapping.ToProfile(_dataContext.Users?.FirstOrDefault(user => user.Id == userId));
        }

        /// <summary>
        /// Найти всех пользователей по имени/фамилии
        /// </summary>
        public List<UserShortModel> GetUsersByName(string name)
        {
            return _dataContext.Users
                .Where(n => n.FirstName.ToLower().Contains(name.ToLower())
                || n.LastName.ToLower().Contains(name.ToLower()))
                .Select(_mapping.ToShortModel)
                .ToList();
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
        /// Создать пользователей массово в БД
        /// </summary>
        public List<UserModel> Create(List<UserModel> users)
        {
            foreach (var user in users)
            {
                var newUser = new User()
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Password = user.Password,
                    Photo = user.Photo,
                    Description = user.Description
                };
                _dataContext.Users.Add(newUser);
            }
            _dataContext.SaveChanges();
            return users;
        }
    }
}