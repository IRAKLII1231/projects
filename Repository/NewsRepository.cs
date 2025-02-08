using Microsoft.EntityFrameworkCore;
using NewsApi.Data;
using NewsApi.Models;
using NewsApi.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsApi.Repository
{
    public class NewsRepository : INewsRepository
    {
        private readonly ApplicationDbContext _context;

        public NewsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<News>> GetAllNews()
        {
            return await _context.News.ToListAsync();
        }

        public async Task<News> GetNewsById(int id)
        {
            return await _context.News.FindAsync(id);
        }

        public async Task AddNews(News news)
        {
            try
            {
                await _context.News.AddAsync(news);
                await _context.SaveChangesAsync();
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
                _context.Entry(news).State = EntityState.Modified;
                await _context.SaveChangesAsync();
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
                var news = await _context.News.FindAsync(id);
                if (news != null)
                {
                    _context.News.Remove(news);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new KeyNotFoundException("News not found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting news: {ex.Message}");
            }
        }
    }
}
