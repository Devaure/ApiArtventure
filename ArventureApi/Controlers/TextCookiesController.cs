#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ArventureApi.Data;
using ArventureApi.Models;

namespace ArventureApi.Controlers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextCookiesController : ControllerBase
    {
        private readonly ArventurebddContext _context;

        public TextCookiesController(ArventurebddContext context)
        {
            _context = context;
        }

        // GET: api/TextCookies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TextCookie>>> GetTextCookies()
        {
            return await _context.TextCookies.ToListAsync();
        }

        // GET: api/TextCookies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TextCookie>> GetTextCookie(int id)
        {
            var textCookie = await _context.TextCookies.FindAsync(id);

            if (textCookie == null)
            {
                return NotFound();
            }

            return textCookie;
        }

        // PUT: api/TextCookies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTextCookie(int id, TextCookie textCookie)
        {
            if (id != textCookie.Id)
            {
                return BadRequest();
            }

            _context.Entry(textCookie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TextCookieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TextCookies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TextCookie>> PostTextCookie(TextCookie textCookie)
        {
            _context.TextCookies.Add(textCookie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTextCookie", new { id = textCookie.Id }, textCookie);
        }

        // DELETE: api/TextCookies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTextCookie(int id)
        {
            var textCookie = await _context.TextCookies.FindAsync(id);
            if (textCookie == null)
            {
                return NotFound();
            }

            _context.TextCookies.Remove(textCookie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TextCookieExists(int id)
        {
            return _context.TextCookies.Any(e => e.Id == id);
        }
    }
}
