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
    public class ObjetTrouvesController : ControllerBase
    {
        private readonly ArventurebddContext _context;

        public ObjetTrouvesController(ArventurebddContext context)
        {
            _context = context;
        }

        // GET: api/ObjetTrouves
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ObjetTrouve>>> GetObjetTrouves()
        {
            return await _context.ObjetTrouves.ToListAsync();
        }

        // GET: api/ObjetTrouves/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ObjetTrouve>> GetObjetTrouve(int id)
        {
            var objetTrouve = await _context.ObjetTrouves.FindAsync(id);

            if (objetTrouve == null)
            {
                return NotFound();
            }

            return objetTrouve;
        }

        // PUT: api/ObjetTrouves/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutObjetTrouve(int id, ObjetTrouve objetTrouve)
        {
            if (id != objetTrouve.Id)
            {
                return BadRequest();
            }

            _context.Entry(objetTrouve).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ObjetTrouveExists(id))
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

        // POST: api/ObjetTrouves
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ObjetTrouve>> PostObjetTrouve(ObjetTrouve objetTrouve)
        {
            _context.ObjetTrouves.Add(objetTrouve);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetObjetTrouve", new { id = objetTrouve.Id }, objetTrouve);
        }

        // DELETE: api/ObjetTrouves/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteObjetTrouve(int id)
        {
            var objetTrouve = await _context.ObjetTrouves.FindAsync(id);
            if (objetTrouve == null)
            {
                return NotFound();
            }

            _context.ObjetTrouves.Remove(objetTrouve);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ObjetTrouveExists(int id)
        {
            return _context.ObjetTrouves.Any(e => e.Id == id);
        }
    }
}
