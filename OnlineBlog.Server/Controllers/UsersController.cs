using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineBlog.Server.Data;
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
        private SubsService _subsService;
        public UsersController(UsersService usersService, SubsService subsService)
        {
            _usersService = usersService;
            _subsService = subsService;
        }

        /// <summary>
        /// Получить профили по имени
        /// </summary>
        [HttpGet("all/{name}")]
        public IActionResult GetUsersByName(string name)
        {
            return Ok(_usersService.GetUsersByName(name));
        }

        /// <summary>
        /// Посмотреть профиль пользователя
        /// </summary>
        [HttpGet("{userId}")]
        public IActionResult GetUserProfileById(Guid userId)
        {
            return Ok(_usersService.GetUserProfileById(userId));
        }

        /// <summary>
        /// Подписаться на пользователя
        /// </summary>
        [HttpPost("subs/{userId}")]
        public IActionResult Subscribe(Guid userId)
        {
            var currentUser = GetCurrentUser();
            if (currentUser == null)
            {
                return NotFound();
            }
            if (currentUser.Id != userId)
            {
                _subsService.Subscribe(currentUser.Id, userId);
            }
            else
            {
                return BadRequest();
            }
            return Ok();
        }

        /// <summary>
        /// Найти текущего пользователя
        /// </summary>
        [HttpGet("GetCurrentUser")]
        public User GetCurrentUser()
        {
            var currentUserEmail = HttpContext.User.Identity.Name;
            var currentUser = _usersService.GetUserByEmail(currentUserEmail);
            return currentUser;
        }
    }
}
