using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineBlog.Server.Services;

namespace OnlineBlog.Server.Controllers
{
    /// <summary>
    /// Контроллер подписок
    /// </summary>
    [ApiController]
    [Authorize] // доступ только авторизованным
    [Route("[controller]")] // маршрут до контроллеров  
    public class SubscribeController : Controller
    {
        private UsersService _usersService;
        private SubsService _subsService;
        public SubscribeController(UsersService usersService, SubsService subsService)
        {
            _usersService = usersService;
            _subsService = subsService;
        }

        /// <summary>
        /// Подписаться на пользователя
        /// </summary>
        [HttpPost("{userId}")]
        public IActionResult Subscribe(Guid userId)
        {
            var currentUser = _usersService.GetUserByEmail(HttpContext.User.Identity.Name);
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
    }
}
