using OnlineBlog.Server.Data;
using OnlineBlog.Server.Models;

namespace OnlineBlog.Server.Helpers
{
    public class Mapping
    {
        private NoSQLDataService _noSQLDataService;
        public Mapping(NoSQLDataService noSQLDataService)
        {
            _noSQLDataService = noSQLDataService;
        }

        /// <summary>
        /// Маппинг модели постов
        /// </summary>
        public NewsModel ToModel(News news)
        {
            var likes = _noSQLDataService.GetNewsLike(news.Id).Users.Count();
            return new NewsModel()
            {
                Id = news.Id,
                Text = news.Text,
                Image = news.Image,
                LikesCount = likes,
                PostDate = news.PostDate
            };
        }

        /// <summary>
        /// Маппинг модели пользователя
        /// </summary>
        public UserModel ToModel(User user)
        {
            return new UserModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = user.Password,
                Description = user.Description,
                Photo = user.Photo
            };
        }
    }
}
