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
        private Mapping _mapping;
        public UsersService(AppDataContext dataContext, Mapping mapping)
        {
            _dataContext = dataContext;
            _mapping = mapping;
        }

        /// <summary>
        /// Создать пользователя
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
        /// Получить пользователя по Email
        /// </summary>
        public User GetUserByEmail(string email)
        {
            return _dataContext.Users.FirstOrDefault(user => user.Email == email);
        }

        /// <summary>
        /// Получить профайл пользователя по Id
        /// </summary>
        public UserProfileModel GetUserProfileById(Guid userId)
        {
            return _mapping.UserToUserProfileModel(_dataContext.Users?.FirstOrDefault(user => user.Id == userId));
        }

        /// <summary>
        /// Найти всех пользователей по имени/фамилии
        /// </summary>
        public List<UserShortModel> GetUsersByName(string name)
        {
            return _dataContext.Users
                .Where(n => n.FirstName.ToLower().Contains(name.ToLower())
                || n.LastName.ToLower().Contains(name.ToLower()))
                .Select(_mapping.UserToUserShortModel)
                .ToList();
        }

        /// <summary>
        /// Редактировать пользователя
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
        /// Удалить пользователя
        /// </summary>
        public void Delete(User user)
        {
            _dataContext.Users.Remove(user);
            _dataContext.SaveChanges();
        }

        /// <summary>
        /// Создать пользователей массово, временно
        /// </summary>
        public List<UserModel> CreateTemp(List<UserModel> users)
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