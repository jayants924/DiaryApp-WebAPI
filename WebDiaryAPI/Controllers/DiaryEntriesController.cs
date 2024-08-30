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

        [HttpGet("{id}")]
        public async Task<ActionResult<DiaryEntry>> GetDiaryEntry(int id)
        {
            var diaryEntry = await _context.DiaryEntries.FindAsync(id);
            if (diaryEntry == null)
            {
                return NotFound();
            }
            return diaryEntry;
        }

        [HttpPost]
        public async Task<ActionResult<DiaryEntry>> PostDiaryEntry(DiaryEntry diaryEntry)
        {
            diaryEntry.Id = 0;
            _context.DiaryEntries.Add(diaryEntry);
            await _context.SaveChangesAsync();
            var resourceUrl = Url.Action(nameof(GetDiaryEntry), new { id = diaryEntry.Id });

            return Created(resourceUrl, diaryEntry);
        }
    }
}
