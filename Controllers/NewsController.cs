using Microsoft.AspNetCore.Mvc;
using NewsApi.Interface;
using NewsApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<News>>> GetNews()
        {
            return Ok(await _newsService.GetAllNews());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<News>> GetNewsItem(int id)
        {
            var news = await _newsService.GetNewsById(id);
            if (news == null) return NotFound();
            return Ok(news);
        }

        [HttpPost]
        public async Task<ActionResult> CreateNews([FromBody] News news)
        {
            if (news == null) return BadRequest("Invalid input data.");

            await _newsService.AddNews(news);
            return CreatedAtAction(nameof(GetNewsItem), new { id = news.Id }, news);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateNews(int id, [FromBody] News news)
        {
            if (id != news.Id) return BadRequest();
            await _newsService.UpdateNews(news);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteNews(int id)
        {
            await _newsService.DeleteNews(id);
            return NoContent();
        }
    }
}
