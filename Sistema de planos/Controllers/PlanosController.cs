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
                    PartidoNombre = p.Partido.Nombre,
                    FechaRetiro = p.FechaRetiro,
                    NombreRetiro = p.NombreRetiro
                }),
                pageIndex,
                pageSize,
                sortColumn,
                sortOrder,
                filterColumn,
                filterQuery
                );


        }


        // PATCH: api/Planos/id -- SOLO CAMBIA LA FECHA DE RETIRO Y EL NOMBRE
        [HttpPatch]
        public async Task<IActionResult> CambiarFechaRetiro(int id, PlanoModelPOST planoM)
        {
            int estado = planoM.EstadoId;
            if (estado.Equals(0)) {
                estado = 24;
            }
            Plano plano = new()
            {
                Id = id,
                NumPlano = planoM.NumPlano,
                Propietario = planoM.Propietario,
                Arancel = planoM.Arancel,
                FechaOriginal = planoM.FechaOriginal,
                EstadoId = estado,
                PartidoId = (int)planoM.PartidoId,
                NombreRetiro = planoM.NombreRetiro,
                FechaRetiro = planoM.FechaRetiro,
            };

            _context.Attach(plano);
            _context.Entry(plano).Property(p => p.FechaRetiro).IsModified = true;
            _context.Entry(plano).Property(p => p.NombreRetiro).IsModified = true;
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


        private void AddHistorico(int id)
        {
            Plano plano2 = _context.Planos.First(p => p.Id == id);
            if(plano2 != null) {
                _context.Historicos.Add(new Historico
                {
                    Observacion = " ",
                    FechaPresentacion = plano2.FechaOriginal,
                    FechaRetiro = plano2.FechaRetiro,
                    NombreRetiro = plano2.NombreRetiro,
                    PlanoId = id,
                    EstadoId = 24
                });
                _context.SaveChanges();
            }
            
        }
    }
}
