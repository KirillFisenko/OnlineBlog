namespace OnlineBlog.Server.Models
{
    /// <summary>
    /// Посты пользователя
    /// </summary>
    public class NewsModel
    {
        /// <summary>
        /// Id поста
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Текст поста
        /// </summary>
        public string? Text { get; set; }

        /// <summary>
        /// Изображение поста
        /// </summary>
        public string? Image { get; set; }

        /// <summary>
        /// Количество лайков у поста
        /// </summary>
        public int? LikesCount { get; set; }

        /// <summary>
        /// Дата публикации поста
        /// </summary>
        public DateTime? PostDate { get; set; }
    }
}
