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
        public Guid Id { get; set; }

        /// <summary>
        /// Список id, на которых подписан пользователь
        /// </summary>
        public List<UserSub>? Users { get; set; }
    }

    public class UserSub
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }
    }
}
