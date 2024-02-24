using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineBlog.Server.Models;
using OnlineBlog.Server.Services;

namespace OnlineBlog.Server.Controllers
{
    /// <summary>
    /// Контроллер постов
    /// </summary>
    [ApiController]
    [Authorize] // доступ только авторизованным
    [Route("[controller]")] // маршрут до контроллеров  
    public class NewsController : Controller
    {
        private NewsService _newsService;
        private UsersService _usersService;

        public NewsController(NewsService newsService, UsersService usersService)
        {
            _newsService = newsService;
            _usersService = usersService;
        }

        #region CRUD
        /// <summary>
        /// Создать пост
        /// </summary>
        [HttpPost]
        public IActionResult Create(NewsModel newsModel)
        {
            var currentUser = _usersService.GetUserByEmail(HttpContext.User.Identity.Name);
            if (currentUser == null)
            {
                return NotFound();
            }
            var newsModelNew = _newsService.Create(newsModel, currentUser.Id);
            return Ok(newsModelNew);
        }

        /// <summary>
        /// Получить посты конкретного пользователя
        /// </summary>
        [HttpGet("{authorId}")]
        public IActionResult GetByAuthor(Guid authorId)
        {
            var news = _newsService.GetByAuthor(authorId);
            return news == null ? NotFound() : Ok(news);
        }

        /// <summary>
        /// Получить посты на основе подписок
        /// </summary>
        [HttpGet]
        public IActionResult GetAll()
        {
            var currentUser = _usersService.GetUserByEmail(HttpContext.User.Identity.Name);
            if (currentUser == null)
            {
                return NotFound();
            }
            var news = _newsService.GetNewsForCurrentUser(currentUser.Id);
            return news == null ? NotFound() : Ok(news);
        }

        /// <summary>
        /// Обновить пост
        /// </summary>
        [HttpPatch]
        public IActionResult Update(NewsModel newsModel)
        {
            var currentUser = _usersService.GetUserByEmail(HttpContext.User.Identity.Name);
            if (currentUser == null)
            {
                return NotFound();
            }
            var newsModelNew = _newsService.Update(newsModel, currentUser.Id);
            return newsModelNew == null ? NotFound() : Ok(newsModelNew);
        }

        /// <summary>
        /// Удалить пост
        /// </summary>
        [HttpDelete("{newsId}")]
        public IActionResult Delete(Guid newsId)
        {
            var currentUser = _usersService.GetUserByEmail(HttpContext.User.Identity.Name);
            if (currentUser == null)
            {
                return NotFound();
            }
            _newsService.Delete(newsId, currentUser.Id);
            return Ok();
        }
        #endregion

        /// <summary>
        /// Создать массово посты, временный
        /// </summary>
        [HttpPost("all")]
        public IActionResult Create(List<NewsModel> newsModels)
        {
            var currentUser = _usersService.GetUserByEmail(HttpContext.User.Identity.Name);
            var newsModelNew = _newsService.Create(newsModels, currentUser.Id);
            return Ok(newsModelNew);
        }
    }
}
