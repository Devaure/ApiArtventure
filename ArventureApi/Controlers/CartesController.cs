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
    public class CartesController : ControllerBase
    {
        private readonly ArventurebddContext _context;

        public CartesController(ArventurebddContext context)
        {
            _context = context;
        }

        // GET: api/Cartes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Carte>>> GetCartes()
        {
            return await _context.Cartes.ToListAsync();
        }

        // GET: api/Cartes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Carte>> GetCarte(int id)
        {
            var carte = await _context.Cartes.FindAsync(id);

            if (carte == null)
            {
                return NotFound();
            }

            return carte;
        }

        // PUT: api/Cartes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarte(int id, Carte carte)
        {
            if (id != carte.Id)
            {
                return BadRequest();
            }

            _context.Entry(carte).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarteExists(id))
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

        // POST: api/Cartes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Carte>> PostCarte(Carte carte)
        {
            _context.Cartes.Add(carte);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CarteExists(carte.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCarte", new { id = carte.Id }, carte);
        }

        // DELETE: api/Cartes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarte(int id)
        {
            var carte = await _context.Cartes.FindAsync(id);
            if (carte == null)
            {
                return NotFound();
            }

            _context.Cartes.Remove(carte);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarteExists(int id)
        {
            return _context.Cartes.Any(e => e.Id == id);
        }
    }
}
