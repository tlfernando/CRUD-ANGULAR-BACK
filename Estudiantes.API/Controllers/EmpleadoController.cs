using Empleados.API.Data;
using Estudiantes.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Empleados.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly EmpleadoData _empleadoData;
        public EmpleadoController(EmpleadoData empleadoData)
        {
            _empleadoData = empleadoData;
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<Empleado> lista = await _empleadoData.Lista();
            return StatusCode(StatusCodes.Status200OK,lista);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Obtener(int id)
        {
            Empleado empleado = await _empleadoData.Obtener(id);
            return StatusCode(StatusCodes.Status200OK,empleado);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Empleado empleado)
        {
            bool respuesta = await _empleadoData.Crear(empleado);
            return StatusCode(StatusCodes.Status200OK, new {isSuccess = respuesta});
        }

        [HttpPut]
        public async Task<IActionResult> Editar(Empleado empleado)
        {
            bool respuesta = await _empleadoData.Editar(empleado);
            return StatusCode(StatusCodes.Status200OK, new {isSuccess = respuesta});
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            bool respuesta = await _empleadoData.Eliminar(id);
            return StatusCode(StatusCodes.Status200OK, new {isSuccess = respuesta});
        }
    }
}
