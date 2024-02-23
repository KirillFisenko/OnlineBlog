using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineBlog.Server.Data;
using OnlineBlog.Server.Services;

namespace OnlineBlog.Server.Controllers
{
    /// <summary>
    /// Контроллер пользователей (найти/подписаться)
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
        /// Найти всех пользователей по имени
        /// </summary>
        [HttpGet("{name}")]
        public IActionResult GetUsersByName(string name)
        {
            return Ok(_usersService.GetUsersByName(name));
        }

        /// <summary>
        /// Подписаться на пользователя
        /// </summary>
        [HttpPost("subs/{userId}")]
        public IActionResult SubscribeUser(Guid userId)
        {
            var currentUser = GetCurrentUser();
            if (currentUser == null)
            {
                return NotFound();
            }
            if (currentUser.Id != userId)
            {
                _usersService.Subscribe(currentUser.Id, userId);
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
        public User GetCurrentUser()
        {
            var currentUserEmail = HttpContext.User.Identity.Name;
            var currentUser = _usersService.GetUserByEmail(currentUserEmail);
            return currentUser;
        }
    }
}
