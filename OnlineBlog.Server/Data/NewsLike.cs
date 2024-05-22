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
        public int NewsId { get; set; }

        /// <summary>
        /// Список id пользователей, которые его лайкнули
        /// </summary>
        public List<int>? Users { get; set; }
    }
}