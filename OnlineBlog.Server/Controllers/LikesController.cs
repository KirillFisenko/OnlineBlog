using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineBlog.Server.Services;

namespace OnlineBlog.Server.Controllers
{
    /// <summary>
    /// Контроллер лайков
    /// </summary>
    [ApiController]
    [Authorize] // доступ только авторизованным
    [Route("[controller]")] // маршрут до контроллеров
    public class LikesController : Controller
    {
        private LikesService _likesService;
        private UsersService _usersService;

        public LikesController(LikesService likesService, UsersService usersService)
        {
            _likesService = likesService;
            _usersService = usersService;
        }

        /// <summary>
        /// Поставить лайк посту
        /// </summary>
        [HttpPost("{newsId}")]
        public IActionResult SetLike(int newsId)
        {
            var currentUser = _usersService.GetUserByEmail(HttpContext.User.Identity.Name);
            if (currentUser == null)
            {
                return NotFound();
            }
            _likesService.SetLike(newsId, currentUser.Id);
            return Ok();
        }
    }
}
