using OnlineBlog.Server.Data;

namespace OnlineBlog.Server.Services
{
    /// <summary>
    /// Сервис подписок
    /// </summary>
    public class SubsService
    {
        private NoSQLDataService _noSQLDataService;

        public SubsService(NoSQLDataService noSQLDataService)
        {
            _noSQLDataService = noSQLDataService;
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
    }
}
