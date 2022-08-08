﻿using System;
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
    public class HistoricosController : ControllerBase
    {
        private readonly PlanosContext _context;

        public HistoricosController(PlanosContext context)
        {
            _context = context;
        }

        // GET: api/Historicos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistoricoModelGET>>> GetHistoricos()
        {
          if (_context.Historicos == null)
          {
              return NotFound();
          }
            return await _context.Historicos.Include(h => h.Plano).Include(h => h.Estado).Select(h => new HistoricoModelGET
            {
                Id = h.Id,
                Observacion = h.Observacion,
                FechaPresentacion = h.FechaPresentacion,
                FechaRetiro = (DateTime)h.FechaRetiro,    
                NombreRetiro = h.NombreRetiro,
                EstadoDescripcion = h.Estado.Codigo
            })
                .ToListAsync();
        }

        // GET: api/Historicos/5 -- POR NUMERO DE PLANO
        [HttpGet("{numPlano}")]
        public async Task<ActionResult<IEnumerable<HistoricoModelGET>>> GetHistorico(int numPlano)
        {
          if (_context.Historicos == null)
          {
              return NotFound();
          }
            return await _context.Historicos.Include(h => h.Plano)
                .Include(h => h.Estado)
                .Where(h => h.Plano.NumPlano == numPlano)
                .Select(h => new HistoricoModelGET
                {
                    Id = h.Id,
                    Observacion = h.Observacion,
                    FechaPresentacion = h.FechaPresentacion,
                    FechaRetiro = (DateTime)h.FechaRetiro,
                    NombreRetiro = h.NombreRetiro != null ? h.NombreRetiro : "",
                    EstadoDescripcion = h.Estado.Codigo
                })
                .ToListAsync();
        }

        // PUT: api/Historicos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHistorico(int id, Historico historico)
        {
            if (id != historico.Id)
            {
                return BadRequest();
            }

            _context.Entry(historico).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HistoricoExists(id))
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

        // POST: api/Historicos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Historico>> PostHistorico(HistoricoModelPOST historicoM)
        {
            Plano plano = _context.Planos.First(p => p.Id == historicoM.planoId);
            Historico historico = new()
            {
                Observacion = historicoM.Observacion,
                FechaPresentacion = plano.FechaOriginal,
                NombreRetiro = plano.NombreRetiro,
                FechaRetiro = plano.FechaRetiro,
                PlanoId = plano.Id,
                EstadoId = plano.EstadoId
            };


            _context.Historicos.Add(historico);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHistorico", new { id = historico.Id }, historico);
        }

        // DELETE: api/Historicos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistorico(int id)
        {
            if (_context.Historicos == null)
            {
                return NotFound();
            }
            var historico = await _context.Historicos.FindAsync(id);
            if (historico == null)
            {
                return NotFound();
            }

            _context.Historicos.Remove(historico);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HistoricoExists(int id)
        {
            return (_context.Historicos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
