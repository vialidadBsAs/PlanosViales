using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema_de_planos.Dominio.Entidades;
using Sistema_de_planos.Infraestructura.Datos;
using Sistema_de_planos.Models.Arancel;

namespace Sistema_de_planos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArancelesController : ControllerBase
    {
        private readonly PlanosContext _context;

        public ArancelesController(PlanosContext context)
        {
            _context = context;
        }

        // GET: api/Aranceles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Arancel>>> GetArancel()
        {
          if (_context.Arancel == null)
          {
              return NotFound();
          }
            return await _context.Arancel.ToListAsync();
        }

        // GET: api/Aranceles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Arancel>> GetArancel(int id)
        {
          if (_context.Arancel == null)
          {
              return NotFound();
          }
            var arancel = await _context.Arancel.FindAsync(id);

            if (arancel == null)
            {
                return NotFound();
            }

            return arancel;
        }

        // PUT: api/Aranceles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArancel(int id, Arancel arancel)
        {
            if (id != arancel.Id)
            {
                return BadRequest();
            }

            _context.Entry(arancel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArancelExists(id))
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

        // POST: api/Aranceles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Arancel>> PostArancel( ArancelPOST arancel)
        {
          if (_context.Arancel == null)
          {
              return Problem("Entity set 'PlanosContext.Arancel'  is null.");
          }
          if ( arancel.ArancelValue < 0 )
            {
                return BadRequest("El valor del arancel debe ser mayor a 0.");
            }
            Arancel arancelToAdd = new Arancel();
            arancelToAdd.Valor = arancel.ArancelValue;
            _context.Arancel.Add(arancelToAdd);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArancel", new { id = arancelToAdd.Id }, arancelToAdd);
        }

        // DELETE: api/Aranceles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArancel(int id)
        {
            if (_context.Arancel == null)
            {
                return NotFound();
            }
            var arancel = await _context.Arancel.FindAsync(id);
            if (arancel == null)
            {
                return NotFound();
            }

            _context.Arancel.Remove(arancel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArancelExists(int id)
        {
            return (_context.Arancel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
