using OnlineBlog.Server.Data;

namespace OnlineBlog.Server.Services
{
    /// <summary>
    /// Сервис лайков
    /// </summary>
    public class LikesService
    {
        private NoSQLDataService _noSQLDataService;

        public LikesService(NoSQLDataService noSQLDataService)
        {
            _noSQLDataService = noSQLDataService;
        }

        /// <summary>
        /// Поставить лайк
        /// </summary>
        /// <param name="newsId">
        /// Id поста, которому ставят лайк
        /// </param>
        /// <param name="userId">
        /// Id пользователя, который ставит лайк
        /// </param>
        public void SetLike(int newsId, int userId)
        {
            _noSQLDataService.SetNewsLike(userId, newsId);
        }
    }
}
