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


        /// <summary>
        /// Создать пост
        /// </summary>
        /// <param name="newsModel">
        /// Пост
        /// </param>
        /// <param name="authorId">
        /// Id пользователя
        /// </param>
        public NewsModel Create(NewsModel newsModel, int authorId)
        {
            var newNews = new News()
            {
                AuthorId = authorId,
                Image = ImageService.GetPhoto(newsModel.Image),
                Text = newsModel.Text,
                PostDate = DateTime.Now
            };
            _dataContext.News.Add(newNews);
            _dataContext.SaveChanges();
            newsModel.Id = newNews.Id;
            newsModel.PostDate = newNews.PostDate;
            return newsModel;
        }

        /// <summary>
        /// Получить пост автора
        /// </summary>
        public List<NewsViewModel> GetByAuthor(int authorId)
        {
            var news = _dataContext.News
                .Where(n => n.AuthorId == authorId)
                .OrderBy(n => n.PostDate)
                .Reverse()
                .Select(_mapping.NewsToNewsViewModel)
                .ToList();
            return news;
        }

        /// <summary>
        /// Получить посты пользователя на основе подписок
        /// </summary>
        public List<NewsViewModel> GetNewsForCurrentUser(int authorId)
        {
            var subs = _noSQLDataService.GetUserSubs(authorId);
            var allNews = new List<NewsViewModel>();
            if (subs != null)
            {
                foreach (var sub in subs.Users)
                {
                    var allNewsByAuthor = _dataContext.News.Where(n => n.AuthorId == sub.Id);
                    allNews.AddRange(allNewsByAuthor.Select(_mapping.NewsToNewsViewModel));
                }
            }
            return allNews.OrderByDescending(n => n.PostDate).ToList();
        }

        /// <summary>
        /// Редактировать пост
        /// </summary>
        /// <param name="newsModel">
        /// Пост
        /// </param>
        /// <param name="authorId">
        /// Id пользователя
        /// </param>
        public NewsViewModel Update(NewsModel newsModel, int authorId)
        {
            var newsToUpdate = _dataContext.News.FirstOrDefault(n => n.Id == newsModel.Id && n.AuthorId == authorId);

            if (newsToUpdate == null)
            {
                return null;
            }

            newsToUpdate.Image = ImageService.GetPhoto(newsModel.Image);
            newsToUpdate.Text = newsModel.Text;
            var photo = ImageService.GetPhoto(newsModel.Image);

            if (!(newsToUpdate.Image.Length > 10 && photo.Length < 10))
            {
                newsToUpdate.Image = photo;
            }

            _dataContext.News.Update(newsToUpdate);
            _dataContext.SaveChanges();
            var newsView = _mapping.NewsToNewsViewModel(newsToUpdate);
            return newsView;
        }

        /// <summary>
        /// Удалить пост
        /// </summary>
        /// <param name="newsId">
        /// Id поста
        /// </param>
        /// <param name="authorId">
        /// Id пользователя
        /// </param>
        public void Delete(int newsId, int authorId)
        {
            var newsToDelete = _dataContext.News.FirstOrDefault(n => n.Id == newsId && n.AuthorId == authorId);
            if (newsToDelete == null)
            {
                return;
            }
            _dataContext.News.Remove(newsToDelete);
            _dataContext.SaveChanges();
        }

        /// <summary>
        /// Создать посты массово, временно
        /// </summary>        
        public List<NewsModel> CreateTemp(List<NewsModel> newsModels, int authorId)
        {
            foreach (var news in newsModels)
            {
                var newNews = new News()
                {
                    AuthorId = authorId,
                    Image = ImageService.GetPhoto(news.Image),
                    Text = news.Text,
                    PostDate = DateTime.Now
                };
                _dataContext.News.Add(newNews);
            }
            _dataContext.SaveChanges();
            return newsModels;
        }
    }
}
