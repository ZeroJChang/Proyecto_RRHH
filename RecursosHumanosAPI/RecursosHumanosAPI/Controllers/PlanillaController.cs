using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RecursosHumanosAPI.Clase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RecursosHumanosAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlanillaController : ControllerBase
    {
        private readonly ILogger<PlanillaController> _logger;
        private readonly string _jsonFilePath = @"C:\Users\Chang\Desktop\Proyecto\Proyecto_RRHH\Trabajadores.json";

        public PlanillaController(ILogger<PlanillaController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "ObtenerEmpleados")]
        [Produces("application/json", Type = typeof(IEnumerable<Empleado>))]
        public IEnumerable<Empleado> ObtenerEmpleados()
        {
            var json = System.IO.File.ReadAllText(_jsonFilePath);
            var empleados = JsonConvert.DeserializeObject<List<Empleado>>(json);
            return empleados;
        }

        [HttpPost(Name = "AgregarEmpleado")]
        [Produces("application/json")]
        public IActionResult AgregarEmpleado([FromBody] Empleado nuevoEmpleado)
        {
            var json = System.IO.File.ReadAllText(_jsonFilePath);
            var empleados = JsonConvert.DeserializeObject<List<Empleado>>(json);

            // Verifica si el NIT del nuevo empleado ya existe
            if (empleados.Any(e => e.NIT == nuevoEmpleado.NIT))
            {
                return Conflict("El empleado ya existe");
            }

            // Agrega el nuevo empleado a la lista
            empleados.Add(nuevoEmpleado);

            // Serializa la lista actualizada y la guarda en el archivo JSON
            var jsonActualizado = JsonConvert.SerializeObject(empleados, Formatting.Indented);
            System.IO.File.WriteAllText(_jsonFilePath, jsonActualizado);

            return Ok();
        }

        [HttpPut("EditarEmpleado/{nit}", Name = "EditarEmpleado")]
        public IActionResult EditarEmpleado(string nit, [FromBody] Empleado empleadoEditado)
        {
            var json = System.IO.File.ReadAllText(_jsonFilePath);
            var empleados = JsonConvert.DeserializeObject<List<Empleado>>(json);

            // Busca el empleado por NIT
            var empleado = empleados.FirstOrDefault(e => e.NIT == nit);

            if (empleado == null)
            {
                return NotFound("Empleado no encontrado");
            }

            // Actualiza los datos del empleado
            empleado.Nombre = empleadoEditado.Nombre;
            empleado.NIT = empleadoEditado.NIT;
            empleado.DPI = empleadoEditado.DPI;
            empleado.SalarioBase = empleadoEditado.SalarioBase;
            empleado.Bono = empleadoEditado.Bono;
            empleado.FechaInicioTrabajo = empleadoEditado.FechaInicioTrabajo;
            empleado.FechaBaja = empleadoEditado.FechaBaja;
            empleado.Direccion = empleadoEditado.Direccion;
            empleado.Area = empleadoEditado.Area;
            empleado.Puesto = empleadoEditado.Puesto;

            // Serializa la lista actualizada y la guarda en el archivo JSON
            var jsonActualizado = JsonConvert.SerializeObject(empleados, Formatting.Indented);
            System.IO.File.WriteAllText(_jsonFilePath, jsonActualizado);

            return Ok("Empleado editado exitosamente");
        }

    }
}
