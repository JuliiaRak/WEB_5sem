﻿using System;
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
    public class ProductionsController : ControllerBase
    {
        private readonly MovieContext _context;

        public ProductionsController(MovieContext context)
        {
            _context = context;
        }

        // GET: api/Productions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Production>>> GetProductions()
        {
            if (_context.Productions == null)
            {
                return NotFound();
            }
            return await _context.Productions.ToListAsync();
        }

        // GET: api/Productions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Production>> GetProduction(int id)
        {
            if (_context.Productions == null)
            {
                return NotFound();
            }
            var production = await _context.Productions.FindAsync(id);

            if (production == null)
            {
                return NotFound();
            }

            return production;
        }

        // PUT: api/Productions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduction(int id, Production production)
        {
            if (id != production.Id)
            {
                return BadRequest();
            }

            _context.Entry(production).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductionExists(id))
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

        // POST: api/Productions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Production>> PostProduction(Production production)
        {
            if (_context.Productions == null)
            {
                return Problem("Entity set 'MovieContext.Productions'  is null.");
            }
            _context.Productions.Add(production);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduction", new { id = production.Id }, production);
        }

        // DELETE: api/Productions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductions(int id)
        {
            if (_context.Productions == null)
            {
                return NotFound();
            }
            var production = await _context.Productions.FindAsync(id);
            if (production == null)
            {
                return NotFound();
            }

            _context.Productions.Remove(production);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductionExists(int id)
        {
            return (_context.Productions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}