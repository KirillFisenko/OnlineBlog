using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineBlog.Server.Data;
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
        public AccountController(UsersService usersService)
        {
            _usersService = usersService;
        }

        #region CRUD
        /// <summary>
        /// Создать пользователя, регистрация
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult<UserModel> Create(UserModel user)
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
            var currentUser = GetCurrentUser();
            return currentUser == null
                ? NotFound()
                : Ok(new UserModel()
                {
                    Id = currentUser.Id,
                    Email = currentUser.Email,
                    FirstName = currentUser.FirstName,
                    LastName = currentUser.LastName,
                    Description = currentUser.Description,
                    Photo = currentUser.Photo
                });
        }

        /// <summary>
        /// Обновить пользователя
        /// </summary>
        [HttpPatch]
        public ActionResult<UserModel> Update(UserModel user)
        {
            var currentUser = GetCurrentUser();
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
            var currentUser = GetCurrentUser();
            if (currentUser == null)
            {
                return NotFound();
            }
            _usersService.Delete(currentUser);
            return Ok();
        }
        #endregion

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