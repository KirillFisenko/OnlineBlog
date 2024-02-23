using OnlineBlog.Server.Models;

namespace OnlineBlog.Server.Helpers
{
    /// <summary>
    /// Сравнение постов для сортировки по убыванию
    /// </summary>
    public class NewsComparer : IComparer<NewsModel>
    {
        public int Compare(NewsModel x, NewsModel y)
        {
            return x.PostDate > y.PostDate ? -1 : x.PostDate < y.PostDate ? 1 : 0;
        }
    }
}
