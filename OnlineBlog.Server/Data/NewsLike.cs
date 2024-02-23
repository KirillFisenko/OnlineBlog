namespace OnlineBlog.Server.Data
{
    /// <summary>
    /// Лайки постов
    /// </summary>
    public class NewsLike
    {
        /// <summary>
        /// id поста
        /// </summary>
        public Guid NewsId { get; set; }

        /// <summary>
        /// список id пользователей, которые его лайкнули
        /// </summary>
        public List<Guid> Users { get; set; }
    }
}