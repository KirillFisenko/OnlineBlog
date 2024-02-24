namespace OnlineBlog.Server.Models
{
    /// <summary>
    /// Укороченная представление пользователя
    /// </summary>
    public class UserShortModel
    {
        /// <summary>
        /// id пользователя
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Описание пользователя
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Фото (аватар) пользователя
        /// </summary>
        public byte[]? Photo { get; set; }
    }
}