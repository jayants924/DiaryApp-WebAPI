using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebDiaryAPI.Data;
using WebDiaryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace WebDiaryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiaryEntriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DiaryEntriesController(ApplicationDbContext context) 
        { 
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiaryEntry>>> GetDiaryEntries()
        {
            return await _context.DiaryEntries.ToListAsync();
        }
    }
}
