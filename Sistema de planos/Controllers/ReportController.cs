using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Reporting.NETCore;
using Sistema_de_planos.Infraestructura.Datos;
using Sistema_de_planos.Models;

namespace Sistema_de_planos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {


        private readonly PlanosContext _context;

        public ReportController(PlanosContext context)
        {
            _context = context;
        }

        [HttpGet("{numPlano}")]
        public FileResult reporteDePlano(int numPlano)
        {
            

            var planos = new List<PlanoModelGET>();

            var plano = _context.Planos
                .Include(p => p.Estado)
                .Include(p => p.Partido)
                .Where(p => p.NumPlano == numPlano)
                .ToList()
                .OrderBy(p => p.NumPlano);


            foreach (var p in plano)
            {
                planos.Add(new()
                {
                    Id = p.Id,
                    NumPlano = p.NumPlano,
                    Propietario = p.Propietario,
                    Arancel = p.Arancel,
                    FechaOriginal = p.FechaOriginal,
                    EstadoDescripcion = p.Estado.Descripcion,
                    PartidoNombre = p.Partido.Nombre,
                    PartidoInmobiliario = p.PartidoInmobiliario,
                    FechaRetiro = p.FechaRetiro,
                    NombreRetiro = p.NombreRetiro,
                    Tipo = p.Tipo
                });
                var historico = _context.Historicos
                .Include(p => p.Plano)
                .Include(e => e.Estado)
                .Where(h => h.PlanoId == p.Id);

                foreach (var h in historico)
                {
                    planos.Add(new()
                    {
                        Id = h.PlanoId,
                        NumPlano = h.Plano.NumPlano,
                        Propietario = h.Plano.Propietario,
                        Arancel = h.Plano.Arancel,
                        FechaOriginal = h.FechaPresentacion,
                        EstadoDescripcion = h.Estado.Descripcion,
                        PartidoNombre = h.Plano.Partido.Nombre,
                        PartidoInmobiliario = h.Plano.PartidoInmobiliario,
                        FechaRetiro = h.FechaRetiro,
                        NombreRetiro = h.NombreRetiro,
                        Tipo = h.Plano.Tipo
                    });
                };


            }
            List<PlanoModelGET> SortedList = planos
                    .OrderBy(o => o.NumPlano)
                    .ThenBy(p => p.FechaOriginal)
                    .ToList();
            Microsoft.Reporting.NETCore.LocalReport report = new Microsoft.Reporting.NETCore.LocalReport();

            //PATH
            var name = "SeguimientoDePlano.rdlc";
            var foldername = Path.Combine("Controllers","Resources", "Reportes");
            var completePath = Path.Combine(Directory.GetCurrentDirectory(), foldername);
            report.ReportPath = Path.Combine(completePath, name);
            //report.ReportPath = path;

            var datasourceAgentes = SortedList;
            //DATASOURCE
            ;
            report.DataSources.Add(new Microsoft.Reporting.NETCore.ReportDataSource("SeguimientoDePlanoDS", datasourceAgentes));

            //parametro
            //string msj = "INFORME BORRADOR";
            //string firma = "";


            //List<ReportParameter> parameters = new List<ReportParameter>();
            //parameters.Add(new ReportParameter("mensaje", msj));
            //parameters.Add(new ReportParameter("firma", firma));
            //report.SetParameters(parameters);

            var result = report.Render("PDF");



            return File(result, "application/pdf", "test.pdf");


        }
    }
}
