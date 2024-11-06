using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RichTextEditorApp.Models;

namespace RichTextEditorApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextContentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TextContentController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAllTextContent()
        {
            var content = _context.TextContents.ToList();
            return content == null ? NotFound() : Ok(content);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTextContent(int id)
        {
            var content = await _context.TextContents.FindAsync(id);
            return content == null ? NotFound() : Ok(content);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTextContent(TextContent textContent)
        {
            _context.TextContents.Add(textContent);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTextContent), new { id = textContent.Id }, textContent);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTextContent(int id, TextContent textContent)
        {
            if (id != textContent.Id) return BadRequest();
            _context.Entry(textContent).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTextContent(int id)
        {
            var content = await _context.TextContents.FindAsync(id);
            if (content == null) return NotFound();
            _context.TextContents.Remove(content);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
