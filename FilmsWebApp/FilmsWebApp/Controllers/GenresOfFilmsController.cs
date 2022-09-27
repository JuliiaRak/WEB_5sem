using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FilmsWebApp.Models;

namespace FilmsWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresOfFilmsController : ControllerBase
    {
        private readonly MovieContext _context;

        public GenresOfFilmsController(MovieContext context)
        {
            _context = context;
        }

        // GET: api/GenresOfFilms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenresOfFilm>>> GetGenresOfFilms()
        {
            return await _context.GenresOfFilms.ToListAsync();
        }

        // GET: api/GenresOfSongs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GenresOfFilm>> GetGenresOfFilm(int id)
        {
            var genresOfFilm = await _context.GenresOfFilms.FindAsync(id);

            if (genresOfFilm == null)
            {
                return NotFound();
            }

            return genresOfFilm;
        }

        // PUT: api/GenresOfFilms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGenresOfFilm(int id, GenresOfFilm genresOfFilm)
        {
            if (id != genresOfFilm.Id)
            {
                return BadRequest();
            }

            _context.Entry(genresOfFilm).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenresOfFilmExists(id))
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

        // POST: api/GenresOfFilms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GenresOfFilm>> PostGenresOfFilm(GenresOfFilm genresOfFilm)
        {
            
            _context.GenresOfFilms.Add(genresOfFilm);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGenresOfFilm", new { id = genresOfFilm.Id }, genresOfFilm);
        }

        // DELETE: api/GenresOfFilms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenresOfFilm(int id)
        {
           
            var genresOfFilm = await _context.GenresOfFilms.FindAsync(id);
            if (genresOfFilm == null)
            {
                return NotFound();
            }

            _context.GenresOfFilms.Remove(genresOfFilm);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GenresOfFilmExists(int id)
        {
           // return (_context.GenresOfFilms?.Any(e => e.Id == id)).GetValueOrDefault();
            return (_context.GenresOfFilms.Any(e => e.Id == id));

        }
    }
}
