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
    public class HistoiresController : ControllerBase
    {
        private readonly ArventurebddContext _context;

        public HistoiresController(ArventurebddContext context)
        {
            _context = context;
        }

        // GET: api/Histoires
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Histoire>>> GetHistoires()
        {
            return await _context.Histoires.ToListAsync();
        }

        // GET: api/Histoires/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Histoire>> GetHistoire(int id)
        {
            var histoire = await _context.Histoires.FindAsync(id);

            if (histoire == null)
            {
                return NotFound();
            }

            return histoire;
        }

        // PUT: api/Histoires/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHistoire(int id, Histoire histoire)
        {
            if (id != histoire.Id)
            {
                return BadRequest();
            }

            _context.Entry(histoire).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HistoireExists(id))
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

        // POST: api/Histoires
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Histoire>> PostHistoire(Histoire histoire)
        {
            _context.Histoires.Add(histoire);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHistoire", new { id = histoire.Id }, histoire);
        }

        // DELETE: api/Histoires/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistoire(int id)
        {
            var histoire = await _context.Histoires.FindAsync(id);
            if (histoire == null)
            {
                return NotFound();
            }

            _context.Histoires.Remove(histoire);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HistoireExists(int id)
        {
            return _context.Histoires.Any(e => e.Id == id);
        }
    }
}
