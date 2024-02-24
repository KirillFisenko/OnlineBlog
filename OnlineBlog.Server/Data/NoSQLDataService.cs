using LiteDB;

namespace OnlineBlog.Server.Data
{
    /// <summary>
    /// NoSQL БД для хранения подписок и лайков
    /// </summary>
    public class NoSQLDataService
    {
        /// <summary>
        /// Путь до БД
        /// </summary>
        private readonly string DBPath = "OnlineBlog_NoSQLDB.db";

        /// <summary>
        /// Коллекция для подписок
        /// </summary>
        private const string SubsCollection = "SubsCollection";

        /// <summary>
        /// Коллекция для лайков
        /// </summary>
        private const string NewsLikesCollection = "NewsLikesCollection";

        /// <summary>
        /// Получить подписки пользователя
        /// </summary>
        /// <param name="userId">id пользователя, подписки которого получить</param>        
        public UserSubs GetUserSubs(Guid userId)
        {
            using (var db = new LiteDatabase(DBPath))
            {
                var subs = db.GetCollection<UserSubs>(SubsCollection);
                var subsForUser = subs.FindOne(u => u.UserId == userId);
                return subsForUser;
            }
        }

        /// <summary>
        /// Подписаться на пользователя
        /// </summary>
        /// <param name="from">кто подписывается</param>       
        /// <param name="to">на кого подписывается</param>  
        public UserSubs SetUserSubs(Guid from, Guid to)
        {
            using (var db = new LiteDatabase(DBPath))
            {
                var subs = db.GetCollection<UserSubs>(SubsCollection);
                var subsForUser = subs.FindOne(u => u.UserId == from);
                if (subsForUser != null)
                {
                    if (!subsForUser.Users.Contains(to))
                    {
                        subsForUser.Users.Add(to);
                        subs.Update(subsForUser);
                    }
                }
                else
                {
                    var newSubsForUser = new UserSubs
                    {
                        UserId = from,
                        Users = [to]
                    };
                    subs.Insert(newSubsForUser);
                    subs.EnsureIndex(u => u.UserId);
                    subsForUser = newSubsForUser;
                }
                return subsForUser;
            }
        }

        /// <summary>
        /// Получить коллекцию лайков поста
        /// </summary>
        /// <param name="newsId">id поста</param>        
        public NewsLike GetNewsLike(Guid newsId)
        {
            using (var db = new LiteDatabase(DBPath))
            {
                var likes = db.GetCollection<NewsLike>(NewsLikesCollection);
                var newsLikes = likes.FindOne(u => u.NewsId == newsId);
                return newsLikes;
            }
        }

        /// <summary>
        /// Поставить лайк посту
        /// </summary>
        /// <param name="from">кто поставил лайк</param>  
        /// /// <param name="newsId">id поста</param> 
        public NewsLike SetNewsLike(Guid from, Guid newsId)
        {
            using (var db = new LiteDatabase(DBPath))
            {
                var likes = db.GetCollection<NewsLike>(NewsLikesCollection);
                var newsLikes = likes.FindOne(u => u.NewsId == newsId);
                if (newsLikes != null)
                {
                    if (!newsLikes.Users.Contains(from))
                    {
                        newsLikes.Users.Add(from);
                        likes.Update(newsLikes);
                    }
                }
                else
                {
                    var newSubsForUser = new NewsLike
                    {
                        NewsId = newsId,
                        Users = [from]
                    };
                    likes.Insert(newSubsForUser);
                    likes.EnsureIndex(u => u.NewsId);
                    newsLikes = newSubsForUser;
                }
                return newsLikes;
            }
        }
    }
}
