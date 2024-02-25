using OnlineBlog.Server.Data;
using OnlineBlog.Server.Models;

namespace OnlineBlog.Server.Helpers
{
    /// <summary>
    /// Маппинг моделей
    /// </summary>
    public class Mapping
    {
        private NoSQLDataService _noSQLDataService;
        public Mapping(NoSQLDataService noSQLDataService)
        {
            _noSQLDataService = noSQLDataService;
        }

        /// <summary>
        /// Маппинг модели поста
        /// </summary>
        public NewsModel NewsToNewsModel(News news)
        {
            var likes = _noSQLDataService.GetNewsLike(news.Id)?.Users?.Count() ?? 0;
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
        /// Маппинг модели профиля
        /// </summary>
        public UserProfileModel UserToUserProfileModel(User user)
        {
            var userSubs = _noSQLDataService.GetUserSubs(user.Id)?.Users?.Count() ?? 0;
            return new UserProfileModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Description = user.Description,
                Photo = user.Photo,
                SubsCount = userSubs
            };
        }

        /// <summary>
        /// Маппинг модели сокращенного представления пользователя
        /// </summary>
        public UserShortModel UserToUserShortModel(User user)
        {
            return new UserShortModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Description = new string(user.Description?.Take(50).ToArray()),
                Photo = user.Photo
            };
        }
    }
}
