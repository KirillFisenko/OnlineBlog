namespace OnlineBlog.Server.Models
{
    /// <summary>
    /// Отображение профиля
    /// </summary>
    public class UserProfileModel
    {
        /// <summary>
        /// Id пользователя
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Email пользователя
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Описание пользователя
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Фото (аватар) пользователя
        /// </summary>
        public object? Photo { get; set; }

        /// <summary>
        /// Количество подписок пользователя
        /// </summary>
        public int? SubsCount { get; set; }
    }
}
