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
    public class LieuxController : ControllerBase
    {
        private readonly ArventurebddContext _context;

        public LieuxController(ArventurebddContext context)
        {
            _context = context;
        }

        // GET: api/Lieux
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lieu>>> GetLieus()
        {
            return await _context.Lieus.ToListAsync();
        }

        // GET: api/Lieux/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lieu>> GetLieu(int id)
        {
            var lieu = await _context.Lieus.FindAsync(id);

            if (lieu == null)
            {
                return NotFound();
            }

            return lieu;
        }

        // GET: api/Lieux/5
        [HttpGet("GetLieuByName")]
        public async Task<ActionResult<Lieu>> GetLieuByName(string endroit)
        {
            var lieu = await _context.Lieus.Where(l => l.Endroit == endroit).FirstOrDefaultAsync();

            return lieu == null ? NotFound() : lieu;
        }

        // PUT: api/Lieux/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLieu(int id, Lieu lieu)
        {
            if (id != lieu.Id)
            {
                return BadRequest();
            }

            _context.Entry(lieu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LieuExists(id))
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

        // POST: api/Lieux
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Lieu>> PostLieu(Lieu lieu)
        {
            _context.Lieus.Add(lieu);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLieu", new { id = lieu.Id }, lieu);
        }

        // DELETE: api/Lieux/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLieu(int id)
        {
            var lieu = await _context.Lieus.FindAsync(id);
            if (lieu == null)
            {
                return NotFound();
            }

            _context.Lieus.Remove(lieu);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LieuExists(int id)
        {
            return _context.Lieus.Any(e => e.Id == id);
        }
    }
}
