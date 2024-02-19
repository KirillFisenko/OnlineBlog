namespace OnlineBlog.Server.Data
{
    /// <summary>
    /// Модель пользователя для БД
    /// </summary> 
    public class User
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
        /// Email пользователя
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Описание пользователя
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Фото пользователя
        /// </summary>
        public byte[]? Photo { get; set; }
    }
}
