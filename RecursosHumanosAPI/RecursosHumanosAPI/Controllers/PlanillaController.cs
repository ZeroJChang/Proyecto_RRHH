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

        [HttpPost("CrearEmpleado", Name = "CrearEmpleado")]
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

        [HttpPost("EditarEmpleado/{nit}", Name = "EditarEmpleado")]
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

        // Método para obtener un empleado por su NIT
        [HttpGet("ObtenerEmpleado/{nit}", Name = "ObtenerEmpleadoPorNIT")]
        [Produces("application/json", Type = typeof(Empleado))]
        public IActionResult ObtenerEmpleadoPorNIT(string nit)
        {
            var json = System.IO.File.ReadAllText(_jsonFilePath);
            var empleados = JsonConvert.DeserializeObject <List<Empleado>>(json);
            var empleado = empleados.FirstOrDefault(e => e.NIT == nit);
            if (empleado == null)
            {
                return NotFound(); // Devolver 404 si no se encuentra el empleado
            }
            return Ok(empleado); // Devolver el empleado encontrado
        }

        [HttpGet("ObtenerEquipo/{area}", Name = "ObtenerEquipo")]
        public IActionResult ObtenerEquipo(string area)
        {
            var json = System.IO.File.ReadAllText(_jsonFilePath);
            var empleados = JsonConvert.DeserializeObject<List<Empleado>>(json);

            var empleadosFiltrados = empleados.Where(e => e.Area == area).ToList();

            if (empleadosFiltrados.Count == 0)
            {
                return NotFound(); // Return 404 if no matching employees are found
            }

            // Create a list to store the transformed JSON responses
            var transformedResponses = new List<EmpleadoRespuesta>();

            DateTime fechaFinal;
            
            // Iterate through each matching employee and build the JSON response
            foreach (var empleado in empleadosFiltrados)
            {
                if (empleado.FechaBaja == null )
                {
                    fechaFinal = DateTime.Now;
                }
                else
                {
                    fechaFinal = (DateTime)empleado.FechaBaja;
                }
                var respuesta = new EmpleadoRespuesta
                {
                    Nombre = empleado.Nombre,
                    SalarioBase = empleado.SalarioBase,
                    FechaInicioTrabajo = empleado.FechaInicioTrabajo,
                    FechaBaja = empleado.FechaBaja,
                    Puesto = empleado.Puesto,
                    Bono14 = Math.Round(empleado.SalarioBase / 365 * new Calculos().CalcularDiasBono14(empleado.FechaInicioTrabajo, fechaFinal), 2),
                    Aguinaldo = Math.Round(empleado.SalarioBase / 365 * new Calculos().CalcularDiasAguinaldo(empleado.FechaInicioTrabajo, fechaFinal), 2),
                    Vacaciones = Math.Round((15M / 30) * (empleado.SalarioBase / 365 * new Calculos().CalcularDias(empleado.FechaInicioTrabajo, fechaFinal)), 2),
                    Indemnizacion = Math.Round(empleado.SalarioBase / 365 * new Calculos().CalcularDias(empleado.FechaInicioTrabajo, fechaFinal), 2),
                };

                transformedResponses.Add(respuesta);
            }

            return Ok(transformedResponses); // Return the list of transformed JSON responses
        }


    }
}
