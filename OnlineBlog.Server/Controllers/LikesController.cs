using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineBlog.Server.Data;
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

        #region CRUD
        /// <summary>
        /// Поставить лайк посту
        /// </summary>
        [HttpPost("{newsId}")]
        public IActionResult SetLike(Guid newsId)
        {
            var currentUser = GetCurrentUser();
            if (currentUser == null)
            {
                return NotFound();
            }
            _likesService.SetLike(newsId, currentUser.Id);
            return Ok();
        }
        #endregion

        /// <summary>
        /// Найти текущего пользователя
        /// </summary>
        [HttpGet("{userId}")]
        public User GetCurrentUser()
        {
            var currentUserEmail = HttpContext.User.Identity.Name;
            var currentUser = _usersService.GetUserByEmail(currentUserEmail);
            return currentUser;
        }
    }
}
