using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema_de_planos.Dominio.Entidades;
using Sistema_de_planos.Infraestructura.Datos;
using Sistema_de_planos.Models;

namespace Sistema_de_planos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartidosController : ControllerBase
    {
        private readonly PlanosContext _context;

        public PartidosController(PlanosContext context)
        {
            _context = context;
        }

        // GET: api/Partidos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Partido>>> GetPartidos()
        {
            if (_context.Partidos == null)
            {
                return NotFound();
            }
            return await _context.Partidos.ToListAsync();
        }

        // GET: api/Partidos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Partido>> GetPartidos(int id)
        {
            if (_context.Partidos == null)
            {
                return NotFound();
            }
            var partidos = await _context.Partidos.FindAsync(id);

            if (partidos == null)
            {
                return NotFound();
            }

            return partidos;
        }

        // PUT: api/Partidos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPartidos(int id, Partido partidos)
        {
            if (id != partidos.Id)
            {
                return BadRequest();
            }

            _context.Entry(partidos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PartidosExists(id))
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

        // POST: api/Partidos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Partido>> PostPartidos(PartidoModelPOST partidoM)
        {
            if (_context.Partidos == null)
            {
                return Problem("Entity set 'PlanosContext.Partidos'  is null.");
            }
            Partido partidos = new();
            partidos.Nombre = partidoM.Nombre;
            partidos.Codigo = partidoM.Codigo;
            _context.Partidos.Add(partidos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPartidos", new { id = partidos.Id }, partidos);
        }

        // DELETE: api/Partidos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePartidos(int id)
        {
            if (_context.Partidos == null)
            {
                return NotFound();
            }
            var partidos = await _context.Partidos.FindAsync(id);
            if (partidos == null)
            {
                return NotFound();
            }

            _context.Partidos.Remove(partidos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PartidosExists(int id)
        {
            return (_context.Partidos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
