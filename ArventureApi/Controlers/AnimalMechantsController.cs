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
    public class AnimalMechantsController : ControllerBase
    {
        private readonly ArventurebddContext _context;

        public AnimalMechantsController(ArventurebddContext context)
        {
            _context = context;
        }

        // GET: api/AnimalMechants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnimalMechant>>> GetAnimalMechants()
        {
            return await _context.AnimalMechants.ToListAsync();
        }

        // GET: api/AnimalMechants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AnimalMechant>> GetAnimalMechant(int id)
        {
            var animalMechant = await _context.AnimalMechants.FindAsync(id);

            if (animalMechant == null)
            {
                return NotFound();
            }

            return animalMechant;
        }

        // PUT: api/AnimalMechants/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnimalMechant(int id, AnimalMechant animalMechant)
        {
            if (id != animalMechant.Id)
            {
                return BadRequest();
            }

            _context.Entry(animalMechant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimalMechantExists(id))
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

        // POST: api/AnimalMechants
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AnimalMechant>> PostAnimalMechant(AnimalMechant animalMechant)
        {
            _context.AnimalMechants.Add(animalMechant);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAnimalMechant", new { id = animalMechant.Id }, animalMechant);
        }

        // DELETE: api/AnimalMechants/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnimalMechant(int id)
        {
            var animalMechant = await _context.AnimalMechants.FindAsync(id);
            if (animalMechant == null)
            {
                return NotFound();
            }

            _context.AnimalMechants.Remove(animalMechant);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AnimalMechantExists(int id)
        {
            return _context.AnimalMechants.Any(e => e.Id == id);
        }
    }
}
