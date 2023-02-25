using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P01WebApi.Models;
using Microsoft.EntityFrameworkCore;

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
        public IActionResult get() {
            List<equipos> listadoEquipo =  (from e in _equiposContext.equipos
                                            where e.estado =="A"
                                            select e).ToList();
            if(listadoEquipo.Count== 0) { return NotFound(); }

            return Ok(listadoEquipo);
        }
        
        //    localhost:4455/api/equipos/getbyid/2
        [HttpGet]
        [Route("getbyid/{id}")]
        public IActionResult get(int id) 
        {
            equipos? unEquipo = (from e in _equiposContext.equipos
                          where e.id_equipos == id && e.estado =="A"
                          select e).FirstOrDefault();
            if(unEquipo == null) 
                return NotFound();
            
            return Ok(unEquipo);
        }

        [HttpGet]
        [Route("find")]
        public IActionResult buscar(string filtro) 
        {
            List<equipos> equiposList = (from e in _equiposContext.equipos
                                         where (e.nombre.Contains(filtro)
                                         || e.descripcion.Contains(filtro))
                                         && e.estado=="A"
                                         select e).ToList();

            if(equiposList.Any())
            {
                return Ok(equiposList);
            }

            return NotFound();

        }
        /// <summary>
        /// Metodo para cear un equipo nuevo
        /// </summary>
        /// <param name="equipoNuevo">clase de equipos</param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        public IActionResult Crear([FromBody] equipos equipoNuevo)
        {
            try
            {
                equipoNuevo.estado = "A";
                _equiposContext.equipos.Add(equipoNuevo);
                _equiposContext.SaveChanges();

                return Ok(equipoNuevo);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult actualizarEquipo(int id, [FromBody]equipos equipoModificar)
        {
            equipos? equipoExiste = (from e in _equiposContext.equipos
                                    where e.id_equipos == id
                                    select e).FirstOrDefault();

            if (equipoExiste == null)
                return NotFound();

            equipoExiste.nombre = equipoModificar.nombre;
            equipoExiste.descripcion = equipoModificar.descripcion;

            _equiposContext.Entry(equipoExiste).State = EntityState.Modified;
            _equiposContext.SaveChanges();

            return Ok(equipoExiste);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult eliminarEquipo(int id)
        {
            equipos? equipoExiste = (from e in _equiposContext.equipos
                                     where e.id_equipos==id
                                     select e).FirstOrDefault();
            if(equipoExiste == null) return NotFound();
            equipoExiste.estado = "I"; 
            
            _equiposContext.Entry(equipoExiste).State = EntityState.Modified;
            //_equiposContext.equipos.Attach(equipoExiste);
            //_equiposContext.equipos.Remove(equipoExiste);
            _equiposContext.SaveChanges();

            return Ok(equipoExiste);
        }


    }
}
