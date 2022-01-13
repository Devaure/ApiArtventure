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
    public class EtatPersonnagesController : ControllerBase
    {
        private readonly ArventurebddContext _context;

        public EtatPersonnagesController(ArventurebddContext context)
        {
            _context = context;
        }

        // GET: api/EtatPersonnages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EtatPersonnage>>> GetEtatPersonnages()
        {
            return await _context.EtatPersonnages.ToListAsync();
        }

        // GET: api/EtatPersonnages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EtatPersonnage>> GetEtatPersonnage(int id)
        {
            var etatPersonnage = await _context.EtatPersonnages.FindAsync(id);

            if (etatPersonnage == null)
            {
                return NotFound();
            }

            return etatPersonnage;
        }

        // PUT: api/EtatPersonnages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEtatPersonnage(int id, EtatPersonnage etatPersonnage)
        {
            if (id != etatPersonnage.Id)
            {
                return BadRequest();
            }

            _context.Entry(etatPersonnage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EtatPersonnageExists(id))
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

        // POST: api/EtatPersonnages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EtatPersonnage>> PostEtatPersonnage(EtatPersonnage etatPersonnage)
        {
            _context.EtatPersonnages.Add(etatPersonnage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEtatPersonnage", new { id = etatPersonnage.Id }, etatPersonnage);
        }

        // DELETE: api/EtatPersonnages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEtatPersonnage(int id)
        {
            var etatPersonnage = await _context.EtatPersonnages.FindAsync(id);
            if (etatPersonnage == null)
            {
                return NotFound();
            }

            _context.EtatPersonnages.Remove(etatPersonnage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EtatPersonnageExists(int id)
        {
            return _context.EtatPersonnages.Any(e => e.Id == id);
        }
    }
}
