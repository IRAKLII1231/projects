using NewsApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsApi.Repositories
{
    public interface INewsRepository
    {
        Task<IEnumerable<News>> GetAllNews();
        Task<News> GetNewsById(int id);
        Task AddNews(News news);
        Task UpdateNews(News news);
        Task DeleteNews(int id);
    }
}
