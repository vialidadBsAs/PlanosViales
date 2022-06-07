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
    public class PlanosController : ControllerBase
    {
        private readonly PlanosContext _context;

        public PlanosController(PlanosContext context)
        {
            _context = context;
        }

        // GET: api/Planoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Plano>>> GetPlanos()
        {
          if (_context.Planos == null)
          {
              return NotFound();
          }
            return await _context.Planos.ToListAsync();
        }

        // GET: api/Planoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Plano>> GetPlano(int id)
        {
          if (_context.Planos == null)
          {
              return NotFound();
          }
            var plano = await _context.Planos.FindAsync(id);

            if (plano == null)
            {
                return NotFound();
            }

            return plano;
        }

        // PUT: api/Planoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlano(int id, Plano plano)
        {
            if (id != plano.Id)
            {
                return BadRequest();
            }

            _context.Entry(plano).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlanoExists(id))
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

        // POST: api/Planoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Plano>> PostPlano(PlanoModelPOST planoM)
        {
          if (_context.Planos == null)
          {
              return Problem("Entity set 'PlanosContext.Planos'  is null.");
          }
            Plano plano = new();
            plano.NumPlano = planoM.NumPlano;
            plano.Propietario = planoM.Propietario;
            plano.Arancel = planoM.Arancel;
            plano.FechaOriginal = DateTime.Now;
            plano.EstadoId = (int) planoM.EstadoId;
            plano.PartidoId = (int) planoM.PartidoId;
            _context.Planos.Add(plano);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlano", new { id = plano.Id }, plano);
        }

        // DELETE: api/Planoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlano(int id)
        {
            if (_context.Planos == null)
            {
                return NotFound();
            }
            var plano = await _context.Planos.FindAsync(id);
            if (plano == null)
            {
                return NotFound();
            }

            _context.Planos.Remove(plano);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlanoExists(int id)
        {
            return (_context.Planos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
