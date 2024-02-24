namespace OnlineBlog.Server.Data
{
    /// <summary>
    /// Лайки постов
    /// </summary>
    public class NewsLike
    {
        /// <summary>
        /// Id поста
        /// </summary>
        public Guid NewsId { get; set; }

        /// <summary>
        /// Список id пользователей, которые его лайкнули
        /// </summary>
        public List<Guid>? Users { get; set; }
    }
}