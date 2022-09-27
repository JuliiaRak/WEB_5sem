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
    public class FilmsController : ControllerBase
    {
        private readonly MovieContext _context;

        public FilmsController(MovieContext context)
        {
            _context = context;
        }

        // GET: api/Songs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Film>>> GetFilms()
        {
            if (_context.Films == null)
            {
                return NotFound();
            }
            return await _context.Films.ToListAsync();
        }

        // GET: api/Films/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Film>> GetFilm(int id)
        {
            if (_context.Films == null)
            {
                return NotFound();
            }
            var film = await _context.Films.FindAsync(id);

            if (film == null)
            {
                return NotFound();
            }

            return film;
        }

        // PUT: api/Films/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFilm(int id, Film film)
        {
            if (id != film.Id)
            {
                return BadRequest();
            }

            _context.Entry(film).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilmExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFilm", new { id = film.Id }, film);
        }

        // POST: api/Films
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Film>> PostFilm(Film film)
        {
            if (_context.Films == null)
            {
                return Problem("Entity set 'MovieContext.Films'  is null.");
            }
            _context.Films.Add(film);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFilm", new { id = film.Id }, film);
        }

        // DELETE: api/Songs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFilm(int id)
        {
            if (_context.Films == null)
            {
                return NotFound();
            }
            var film = await _context.Films.FindAsync(id);
            if (film == null)
            {
                return NotFound();
            }

            _context.Films.Remove(film);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FilmExists(int id)
        {
            return (_context.Films?.Any(e => e.Id == id)).GetValueOrDefault();
        }




        /*// GET: api/Songs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FilmsOutputDTO>>> GetFilms()
        {
            var list = await _context.Films.ToListAsync();
            var result = new List<FilmsOutputDTO>();
            foreach (var film in list)
            {
                FilmsOutputDTO output = new FilmsOutputDTO()
                {
                    Name = film.Name,
                    ProductionName = _context.Productions.FirstOrDefault(a => a.Id == film.ProductionId).Name,
                    Duration = film.Duration,
                    Id = film.Id
                };
                result.Add(output);

            }
            return result;
            //return await _context.Films.ToListAsync();
        }

        // GET: api/Films/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Film>> GetFilm(int id)
        {
            
            var film = await _context.Films.FindAsync(id);

            if (film == null)
            {
                return NotFound();
            }
            FilmsOutputDTO result = new FilmsOutputDTO()
            {
                Name = film.Name,
                ProductionName = _context.Productions.FirstOrDefault(a => a.Id == film.ProductionId).Name,
                Duration = film.Duration,
                Id = film.Id
            };
            return Ok(result);
            //return film;
        }

        // PUT: api/Films/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFilm(int id, FilmsInputDTO filmIn)
        {
            filmIn.Id = id;
            Film film = await _context.Films.FindAsync(id);
            film.Name = filmIn.Name;
            film.ProductionId = filmIn.ProductionId;
            film.Duration = filmIn.Duration;
            _context.Entry(filmIn).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilmExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("GetFilm", new { id = film.Id });
            //return NoContent();
        }

        // POST: api/Films
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Film>> PostFilm(FilmsInputDTO filmDTO)
        {
            Film film = new Film()
            {
                Name = filmDTO.Name,
                Duration = filmDTO.Duration,
                ProductionId = filmDTO.ProductionId
            };
            film.Production = _context.Productions.FirstOrDefault(a => a.Id == filmDTO.ProductionId);
            _context.Films.Add(film);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFilm", new { id = film.Id }, film);
        }

        // DELETE: api/Songs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFilm(int id)
        {
            
            var film = await _context.Films.FindAsync(id);
            if (film == null)
            {
                return NotFound();
            }

            _context.Films.Remove(film);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FilmExists(int id)
        {
            //return (_context.Films?.Any(e => e.Id == id)).GetValueOrDefault();
            return (_context.Films.Any(e => e.Id == id));

        }*/
    }
}
