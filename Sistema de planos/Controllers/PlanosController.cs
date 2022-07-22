using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
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
            if (filterQuery == "null") {
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
                    PartidoNombre = p.Partido.Nombre,
                    FechaRetiro = p.FechaRetiro,
                    NombreRetiro = p.NombreRetiro,
                    Tipo = p.Tipo,
                    TieneHistoricos = p.Historicos.Count() != 0,
                    PartidoId = (int)p.PartidoId
        }),
                pageIndex,
                pageSize,
                sortColumn,
                sortOrder,
                filterColumn,
                filterQuery
                );


        }

        // GET: api/Planos/1
        [HttpGet("{id}")]
        public async Task<ActionResult<PlanoModelGET>> GetPlanos(int id)
        {
            if (_context.Planos == null)
            {
                return NotFound();
            }
            var p = await _context.Planos.Include(p => p.Estado).Include(p => p.Partido).Where(p => p.Id == id).FirstOrDefaultAsync();
            PlanoModelGET plano;

            if (p == null)
            {
                return NotFound();
            } else
            {
                plano = new()
                {
                    Id = p.Id,
                    NumPlano = p.NumPlano,
                    Propietario = p.Propietario,
                    Arancel = p.Arancel,
                    FechaOriginal = p.FechaOriginal,
                    EstadoDescripcion = p.Estado.Descripcion,
                    PartidoNombre = p.Partido.Nombre,
                    FechaRetiro = p.FechaRetiro,
                    NombreRetiro = p.NombreRetiro,
                    Tipo = p.Tipo
                };
            }

            return plano;

        }


        // PATCH: api/Planos/id -- SOLO CAMBIA LA FECHA DE RETIRO, EL NOMBRE Y EL ESTADO
        [HttpPut]
        public async Task<IActionResult> CambiarFechaRetiro(int id, PlanoModelPOST planoM)
        {
            Plano plano = new()
            {
                Id = id,
                NumPlano = 0,
                Propietario = planoM.Propietario,
                Arancel = planoM.Arancel,
                FechaOriginal = planoM.FechaOriginal != null ? planoM.FechaOriginal : DateTime.Now,
                EstadoId = planoM.EstadoId,
                PartidoId = planoM.PartidoId,
                NombreRetiro = planoM.NombreRetiro,
                FechaRetiro = planoM.FechaRetiro,
                Tipo = planoM.Tipo
            };

            _context.Attach(plano);
            if (planoM.FechaRetiro != null) _context.Entry(plano).Property(p => p.FechaRetiro).IsModified = true;
            if (planoM.NombreRetiro != null) _context.Entry(plano).Property(p => p.NombreRetiro).IsModified = true;
            if(planoM.EstadoId != 0) _context.Entry(plano).Property(p => p.EstadoId).IsModified = true;
            if (planoM.PartidoId != 0) _context.Entry(plano).Property(p => p.PartidoId).IsModified = true;
            if (planoM.Propietario != "") _context.Entry(plano).Property(p => p.Propietario).IsModified = true;
            if (planoM.Arancel != 0) _context.Entry(plano).Property(p => p.Arancel).IsModified = true;
            if (planoM.Tipo != "") _context.Entry(plano).Property(p => p.Tipo).IsModified = true;
            _context.SaveChanges();
            return NoContent();

        }

        // PATCH: api/Planos/id -- SOLO CAMBIA LA FECHA DE RETIRO Y EL NOMBRE
        [HttpPut]
        [Route("/reingreso")]
        public async Task<IActionResult> Reingreso(int id, PlanoModelPOST planoM)
        {
            Plano plano = _context.Planos.First(p => p.Id == id);
            if (plano.NombreRetiro == null | plano.FechaRetiro == null) return BadRequest("Necesita haber un retiro para reingresar.");

            Historico historico = new()
            {
                Observacion = "",
                FechaPresentacion = plano.FechaOriginal,
                NombreRetiro = plano.NombreRetiro != "" ? plano.NombreRetiro : "",
                FechaRetiro = plano.FechaRetiro,
                EstadoId = plano.EstadoId
            };

            plano.Historicos.Add(historico);

            plano.EstadoId = 25; // EL 25 ES EL REINGRESADO
            plano.FechaRetiro = null;
            plano.NombreRetiro = null;
            plano.FechaOriginal = DateTime.Now;

           

            _context.Attach(plano);
            _context.Entry(plano).Property(p => p.FechaRetiro).IsModified = true;
            _context.Entry(plano).Property(p => p.NombreRetiro).IsModified = true;
            _context.Entry(plano).Property(p => p.FechaOriginal).IsModified = true;
            _context.Entry(plano).Property(p => p.EstadoId).IsModified = true;
            _context.SaveChanges();
            return NoContent();

        }
        // POST: api/Planos
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
                PartidoId = (int)planoM.PartidoId,
                NombreRetiro = planoM.NombreRetiro,
                FechaRetiro = planoM.FechaRetiro,
                Historicos = new List<Historico>(),
                Tipo = planoM.Tipo
            };
            _context.Planos.Add(plano);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlanos", new { id = plano.Id }, plano);
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

        [HttpPost]
        [Route("IsDupeField")]
        public bool IsDupeField(PlanoModelGET plano)
        {
            return _context.Planos.Any(
                e => e.NumPlano == plano.NumPlano && e.Id != plano.Id);
        }


        [HttpGet("lastPlanoNumber")]
        public async Task<int> GetLastNroPlano()
        {
            int table_size;
            
            try
            {
                LastPlanoNumber actualRow = _context.LastPlanoNumber.First();               
                //_context.LastPlanoNumber.Attach(actualRow);
                _context.Entry(actualRow).State = EntityState.Modified;
                actualRow.LastNroPlano = actualRow.LastNroPlano + 1;
                table_size = actualRow.LastNroPlano;

            }
            catch (InvalidOperationException) // En caso que la fila aun no exista, se levanta la excepcion
            {
                table_size = _context.Planos.CountAsync().Result + 2003;
                LastPlanoNumber createRow = new LastPlanoNumber
                {
                    LastNroPlano = table_size
                };
            _context.LastPlanoNumber.Add(createRow);
            }
            await _context.SaveChangesAsync();
            return table_size;
        }

        [HttpGet("estadosStats")]
       public async Task<ActionResult<IEnumerable<EstadoModelSTATS>>> GetEstadosStats(DateTime d1, DateTime d2)
       {
           if (_context.Planos == null)
           {
               return null;
           }

           if(d1 == null || d2 == null)
            {
                return BadRequest("Debes ingresar una fecha para ver las estadisticas.");
            }

            if (d1 > DateTime.Today || d2 > DateTime.Today)
            {
                return BadRequest("No se pueden obtener estadisticas de una fecha posterior a hoy.");
            }

            if (d1 > d2)
            {
                return BadRequest("Ingreso de fechas inválido. Recorda que la fecha de inicio nunca puede ser posterior a la fecha de fin.");
            }

            var sql = (from p in _context.Planos
                       where p.FechaOriginal >= d1 && p.FechaOriginal <= d2
                       orderby p.EstadoId
                       group p by p.EstadoId into estados
                       select new EstadoModelSTATS
                       {
                           Id = estados.Key,
                           Cant = estados.Count(),
                           TotalArancel = (from p in _context.Planos
                                           join e in _context.Estados on p.EstadoId equals e.Id
                                           where e.Id == estados.Key && (p.FechaOriginal >= d1 && p.FechaOriginal <= d2)
                                           select p.Arancel).Sum()
                       }).ToListAsync();

            return await sql;
       }

        [HttpPost]
        [Route("IsValidDate")]
        public bool IsValidDate(PlanoModelGET plano)
        {
            int res = DateTime.Compare(plano.FechaOriginal, DateTime.Today);
            return res <= 0;
        }

    }
}
