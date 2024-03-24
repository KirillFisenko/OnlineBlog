namespace OnlineBlog.Server.Models
{
    public class NewsViewModel
    {
        /// <summary>
        /// Id поста
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Текст поста
        /// </summary>
        public string? Text { get; set; }

        /// <summary>
        /// Изображение поста
        /// </summary>
        public object? Image { get; set; }

        /// <summary>
        /// Количество лайков у поста
        /// </summary>
        public int? LikesCount { get; set; }

        /// <summary>
        /// Дата публикации поста
        /// </summary>
        public DateTime PostDate { get; set; }
    }
}
