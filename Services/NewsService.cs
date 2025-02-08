using NewsApi.Interface;
using NewsApi.Models;
using NewsApi.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsApi.Services
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsRepository;

        public NewsService(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        public async Task<IEnumerable<News>> GetAllNews()
        {
            return await _newsRepository.GetAllNews();
        }

        public async Task<News> GetNewsById(int id)
        {
            return await _newsRepository.GetNewsById(id);
        }

        public async Task AddNews(News news)
        {
            try
            {
                if (string.IsNullOrEmpty(news.Title) || string.IsNullOrEmpty(news.Description))
                {
                    throw new ArgumentException("Title and Description cannot be empty.");
                }
                await _newsRepository.AddNews(news);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding news: {ex.Message}");
            }
        }

        public async Task UpdateNews(News news)
        {
            try
            {
                await _newsRepository.UpdateNews(news);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating news: {ex.Message}");
            }
        }

        public async Task DeleteNews(int id)
        {
            try
            {
                await _newsRepository.DeleteNews(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting news: {ex.Message}");
            }
        }
    }
}
