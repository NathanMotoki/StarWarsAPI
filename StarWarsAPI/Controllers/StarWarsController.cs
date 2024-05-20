using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarWarsAPI.Context;
using StarWarsAPI.Models;

namespace StarWarsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StarWarsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StarWarsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/StarWars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StarWars>>> GetstarWars()
        {
            return await _context.starWars.ToListAsync();
        }

        // GET: api/StarWars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StarWars>> GetStarWars(int id)
        {
            var starWars = await _context.starWars.FindAsync(id);

            if (starWars == null)
            {
                return NotFound();
            }

            return starWars;
        }

        // PUT: api/StarWars/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStarWars(int id, StarWars starWars)
        {
            if (id != starWars.Id)
            {
                return BadRequest();
            }

            _context.Entry(starWars).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StarWarsExists(id))
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

        // POST: api/StarWars
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StarWars>> PostStarWars(StarWars starWars)
        {
            _context.starWars.Add(starWars);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStarWars", new { id = starWars.Id }, starWars);
        }

        // DELETE: api/StarWars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStarWars(int id)
        {
            var starWars = await _context.starWars.FindAsync(id);
            if (starWars == null)
            {
                return NotFound();
            }

            _context.starWars.Remove(starWars);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StarWarsExists(int id)
        {
            return _context.starWars.Any(e => e.Id == id);
        }
    }
}
