using OnlineBlog.Server.Data;
using OnlineBlog.Server.Helpers;
using OnlineBlog.Server.Models;

namespace OnlineBlog.Server.Services
{
    /// <summary>
    /// Сервис постов
    /// </summary>
    public class NewsService
    {
        private AppDataContext _dataContext;
        private NoSQLDataService _noSQLDataService;
        private Mapping _mapping;
        public NewsService(AppDataContext dataContext, NoSQLDataService noSQLDataService, Mapping mapping)
        {
            _dataContext = dataContext;
            _noSQLDataService = noSQLDataService;
            _mapping = mapping;
        }

        #region CRUD
        /// <summary>
        /// Создать публикацию в БД
        /// </summary>
        /// <param name="newsModel">
        /// Публикация (пост)
        /// </param>
        /// <param name="authorId">
        /// Id пользователя
        /// </param>
        public NewsModel Create(NewsModel newsModel, Guid authorId)
        {
            var newNews = new News()
            {
                AuthorId = authorId,
                Image = newsModel.Image,
                Text = newsModel.Text,
                PostDate = DateTime.Now
            };
            _dataContext.News.Add(newNews);
            _dataContext.SaveChanges();
            newsModel.Id = newNews.Id;
            newsModel = _mapping.ToModel(newNews);
            return newsModel;
        }

        /// <summary>
        /// Получить публикации автора из БД
        /// </summary>
        public List<NewsModel> GetByAuthor(Guid authorId)
        {
            var news = _dataContext.News
                .Where(n => n.AuthorId == authorId)
                .OrderBy(n => n.PostDate)
                .Reverse()
                .Select(_mapping.ToModel)
                .ToList();
            return news;
        }

        /// <summary>
        /// Получить подписки пользователя из БД NoSQL
        /// </summary>
        public List<NewsModel> GetNewsForCurrentUser(Guid authorId)
        {
            var subs = _noSQLDataService.GetUserSubs(authorId);
            var allNews = new List<NewsModel>();
            if (subs != null)
            {
                foreach (var sub in subs.Users)
                {
                    var allNewsByAuthor = _dataContext.News.Where(n => n.AuthorId == sub);
                    allNews.AddRange(allNewsByAuthor.Select(_mapping.ToModel));
                }
            }
            allNews.Sort(new NewsComparer());
            return allNews;
        }

        /// <summary>
        /// Обновить публикацию в БД
        /// </summary>
        /// <param name="newsModel">
        /// Публикация (пост)
        /// </param>
        /// <param name="authorId">
        /// Id пользователя
        /// </param>
        public NewsModel Update(NewsModel newsModel, Guid authorId)
        {
            var newsToUpdate = _dataContext.News.FirstOrDefault(n => n.Id == newsModel.Id && n.AuthorId == authorId);
            if (newsToUpdate == null)
            {
                return null;
            }
            newsToUpdate.Image = newsModel.Image;
            newsToUpdate.Text = newsModel.Text;
            _dataContext.News.Update(newsToUpdate);
            _dataContext.SaveChanges();
            newsModel = _mapping.ToModel(newsToUpdate);
            return newsModel;
        }

        /// <summary>
        /// Удалить публикацию в БД
        /// </summary>
        /// <param name="newsModel">
        /// Публикация (пост)
        /// </param>
        /// <param name="authorId">
        /// Id пользователя
        /// </param>
        public void Delete(Guid newsId, Guid authorId)
        {
            var newsToDelete = _dataContext.News.FirstOrDefault(n => n.Id == newsId && n.AuthorId == authorId);
            if (newsToDelete == null)
            {
                return;
            }
            _dataContext.News.Remove(newsToDelete);
            _dataContext.SaveChanges();
        }
        #endregion        
    }
}
