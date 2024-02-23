namespace OnlineBlog.Server.Data
{
    /// <summary>
    /// Пост пользователя
    /// </summary>
    public class News
    {
        /// <summary>
        /// Id поста
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Id автора поста
        /// </summary>
        public Guid AuthorId { get; set; }

        /// <summary>
        /// Текст поста
        /// </summary>
        public string? Text { get; set; }

        /// <summary>
        /// Изображение поста
        /// </summary>
        public byte[]? Image { get; set; }

        /// <summary>
        /// Количество лайков у поста
        /// </summary>
        public int LikesCount { get; set; }

        /// <summary>
        /// Дата публикации поста
        /// </summary>
        public DateTime PostDate { get; set; }
    }
}
