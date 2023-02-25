using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P01WebApi.Models;

namespace P01WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquiposController : ControllerBase
    {
        private readonly equiposContext _equiposContext;

        public EquiposController(equiposContext equiposContext)
        {
            _equiposContext = equiposContext;
        }

        [HttpGet]
        [Route("getall")]
        public IActionResult ObtenerEquipos() {
            List<equipos> listadoEquipo =  (from e in _equiposContext.equipos
                                            select e).ToList();
            if(listadoEquipo.Count== 0) { return NotFound(); }

            return Ok(listadoEquipo);
        }

    }
}
