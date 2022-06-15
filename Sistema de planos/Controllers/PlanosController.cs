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

        // GET: api/Planos
        [HttpGet]
        //[Route("{pageIndex?}/{pageSize?}")]
        public async Task<ActionResult<ApiResult<PlanoModelGET>>> GetPlanos(
            int pageIndex = 0, 
            int pageSize = 10,
            string sortColumn = null,
            string sortOrder = null,
            string filterColumn = null,
            string filterQuery = null)
        {
            if(filterQuery == "null") {
                filterQuery = null;
            }
            
            return await ApiResult<PlanoModelGET>.CreateAsync(
                _context.Planos.Include(p => p.Estado).Include(p => p.Partido).Select(p => new PlanoModelGET
                {
                    Id = p.Id,
                    NumPlano = p.NumPlano,
                    Propietario = p.Propietario,
                    Arancel = p.Arancel,
                    FechaOriginal = p.FechaOriginal,
                    EstadoDescripcion = p.Estado.Descripcion,
                    PartidoNombre = p.Partido.Nombre
                }),
                pageIndex,
                pageSize,
                sortColumn,
                sortOrder,
                filterColumn,
                filterQuery
                );


        }

        // GET: api/Planos/11 -- POR NUMERO DE PLANO
        [HttpGet("numPlano/{numPlano}")]
        public async Task<ActionResult<IEnumerable<PlanoModelGET>>> GetPlanoNum(int numPlano)
        {
            if (_context.Planos == null)
            {
                return NotFound();
            }

            return await _context.Planos.Include(p => p.Estado).Include(p => p.Partido).Where(p => p.NumPlano == numPlano)
                .Select(p => new PlanoModelGET
                {
                    Id = p.Id,
                    NumPlano = p.NumPlano,
                    Propietario = p.Propietario,
                    Arancel = p.Arancel,
                    FechaOriginal = p.FechaOriginal,
                    EstadoDescripcion = p.Estado.Descripcion,
                    PartidoNombre = p.Partido.Nombre
                }).
                ToListAsync();
        }

        // GET: api/Planos/Ezequiel --POR PROPIETARIO
        [HttpGet("propietario/{propietario}")]
        public async Task<ActionResult<IEnumerable<PlanoModelGET>>> GetPlanoProp(string propietario)
        {
            if (_context.Planos == null)
            {
                return NotFound();
            }

            return await _context.Planos.Include(p => p.Estado).Include(p => p.Partido).Where(p => p.Propietario.Contains(propietario))
                .Select(p => new PlanoModelGET
                {
                    Id = p.Id,
                    NumPlano = p.NumPlano,
                    Propietario = p.Propietario,
                    Arancel = p.Arancel,
                    FechaOriginal = p.FechaOriginal,
                    EstadoDescripcion = p.Estado.Descripcion,
                    PartidoNombre = p.Partido.Nombre
                }).
                ToListAsync();
        }

        // GET: api/Planos/Berazategui --POR NOMBRE DE PARTIDO
        [HttpGet("partido/{partido}")]
        public async Task<ActionResult<IEnumerable<PlanoModelGET>>> GetPlanoPartido(string partido)
        {
            if (_context.Planos == null)
            {
                return NotFound();
            }

            return await _context.Planos.Include(p => p.Estado)
                .Include(p => p.Partido)
                .Where(p => p.Partido.Nombre == partido)
                .Select(p => new PlanoModelGET
                {
                    Id = p.Id,
                    NumPlano = p.NumPlano,
                    Propietario = p.Propietario,
                    Arancel = p.Arancel,
                    FechaOriginal = p.FechaOriginal,
                    EstadoDescripcion = p.Estado.Descripcion,
                    PartidoNombre = p.Partido.Nombre
                })
                .ToListAsync();
        }

        // GET: api/Planos/2 --POR PARTIDO ID
        [HttpGet("partidoId/{partidoId}")]
        public async Task<ActionResult<IEnumerable<PlanoModelGET>>> GetPlanoPartidoId(int partidoId)
        {
            if (_context.Planos == null)
            {
                return NotFound();
            }

            return await _context.Planos.Include(p => p.Estado).Include(p => p.Partido).Where(p => p.PartidoId == partidoId)
                .Select(p => new PlanoModelGET
                {
                    Id = p.Id,
                    NumPlano = p.NumPlano,
                    Propietario = p.Propietario,
                    Arancel = p.Arancel,
                    FechaOriginal = p.FechaOriginal,
                    EstadoDescripcion = p.Estado.Descripcion,
                    PartidoNombre = p.Partido.Nombre
                }).
                ToListAsync();
        }


        // PUT: api/Planoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("id")]
        public async Task<IActionResult> PutPlano(int id, PlanoModelPOST planoM)
        {
            Plano plano = new()
            {
                Id = id,
                NumPlano = planoM.NumPlano,
                Propietario = planoM.Propietario,
                Arancel = planoM.Arancel,
                FechaOriginal = planoM.FechaOriginal,
                EstadoId = (int)planoM.EstadoId,
                PartidoId = (int)planoM.PartidoId
            };
            _context.Entry(plano).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlanoExists(planoM.NumPlano))
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
            Plano plano = new()
            {
                NumPlano = planoM.NumPlano,
                Propietario = planoM.Propietario,
                Arancel = planoM.Arancel,
                FechaOriginal = planoM.FechaOriginal,
                EstadoId = (int)planoM.EstadoId,
                PartidoId = (int)planoM.PartidoId
            };
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
