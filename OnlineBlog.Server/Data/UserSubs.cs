namespace OnlineBlog.Server.Data
{
    /// <summary>
    /// Подписки пользователя
    /// </summary>
    public class UserSubs
    {
        /// <summary>
        /// Id пользователя
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Список id, на которых подписан пользователь
        /// </summary>
        public List<Guid>? Users { get; set; }
    }
}
