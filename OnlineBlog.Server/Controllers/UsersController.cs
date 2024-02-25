using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineBlog.Server.Models;
using OnlineBlog.Server.Services;

namespace OnlineBlog.Server.Controllers
{
    /// <summary>
    /// Контроллер пользователей
    /// </summary>
    [ApiController]
    [Authorize] // доступ только авторизованным
    [Route("[controller]")] // маршрут до контроллеров 
    public class UsersController : Controller
    {
        private UsersService _usersService;
        public UsersController(UsersService usersService)
        {
            _usersService = usersService;
        }

        /// <summary>
        /// Получить профили по имени
        /// </summary>
        [HttpGet("all/{name}")]
        public IActionResult GetUsersByName(string name)
        {
            var user = _usersService.GetUsersByName(name);
            return user == null ? NotFound() : Ok(user);
        }

        /// <summary>
        /// Посмотреть профиль пользователя
        /// </summary>
        [HttpGet("{userId}")]
        public IActionResult GetUserProfileById(Guid userId)
        {
            var user = _usersService.GetUserProfileById(userId);
            return user == null ? NotFound() : Ok(user);
        }

        /// <summary>
        /// Создать массово пользователей, временный
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        public IActionResult CreateUsersTemp(List<UserModel> users)
        {
            var newUsers = _usersService.CreateTemp(users);
            return Ok(newUsers);
        }
    }
}
