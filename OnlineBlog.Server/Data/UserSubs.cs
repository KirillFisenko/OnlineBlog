namespace OnlineBlog.Server.Data
{
    /// <summary>
    /// Подписки пользователя
    /// </summary>
    public class UserSubs
    {
        /// <summary>
        /// Id подписки
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Список id, на которых подписан пользователь
        /// </summary>
        public List<UserSub>? Users { get; set; }
    }

    public class UserSub
    {
        /// <summary>
        /// Id пользователя
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Дата подписки
        /// </summary>
        public DateTime Date { get; set; }
    }
}
