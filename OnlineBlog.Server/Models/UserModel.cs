using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;

namespace OnlineBlog.Server.Models
{
    /// <summary>
    /// Пользователь
    /// </summary> 
    public class UserModel
    {
        /// <summary>
        /// Id пользователя
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
        /// Фото (аватар) пользователя
        /// </summary>
        public string Photo { get; set; }

        public byte[] GetPhoto()
        {
            try
            {
                return JsonConvert.DeserializeObject<byte[]>(Photo);
            }
            catch
            {
                try
                {
                    return JsonConvert.DeserializeObject<byte[]>("[" + Photo + "]");
                }
                catch
                {
                    return Array.Empty<byte>();
                }
            }
        }
    }
}