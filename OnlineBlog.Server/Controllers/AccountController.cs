using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineBlog.Server.Helpers;
using OnlineBlog.Server.Models;
using OnlineBlog.Server.Services;

namespace OnlineBlog.Server.Controllers
{
    /// <summary>
    /// Контроллер аккаунтов
    /// </summary>
    [ApiController]
    [Authorize] // доступ только авторизованным
    [Route("[controller]")] // маршрут до контроллеров   
    public class AccountController : Controller
    {
        private UsersService _usersService;
        private Mapping _mapping;
        public AccountController(UsersService usersService, Mapping mapping)
        {
            _usersService = usersService;
            _mapping = mapping;
        }

        /// <summary>
        /// Создать пользователя, регистрация
        /// </summary>
        [HttpPost]
        [AllowAnonymous] // можно незарегистрированным пользователям
        public IActionResult Create(UserModel user)
        {
            var newUser = _usersService.Create(user);
            return Ok(newUser);
        }

        /// <summary>
        /// Получить пользователя, посмотреть профиль
        /// </summary>
        [HttpGet]
        public IActionResult Get()
        {
            var currentUser = _usersService.GetUserByEmail(HttpContext.User.Identity.Name);
            return currentUser == null
                ? NotFound()
                : Ok(_mapping.UserToUserProfileModel(currentUser));
        }

        /// <summary>
        /// Редактировать пользователя
        /// </summary>
        [HttpPatch]
        public IActionResult Update(UserModel user)
        {
            var currentUser = _usersService.GetUserByEmail(HttpContext.User.Identity.Name);
            if (currentUser == null)
            {
                return NotFound();
            }
            _usersService.Update(user);
            return Ok(user);
        }

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        [HttpDelete]
        public IActionResult Delete()
        {
            var currentUser = _usersService.GetUserByEmail(HttpContext.User.Identity.Name);
            if (currentUser == null)
            {
                return NotFound();
            }
            _usersService.Delete(currentUser);
            return Ok();
        }
    }
}