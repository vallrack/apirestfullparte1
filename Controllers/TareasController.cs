using Microsoft.AspNetCore.Mvc;
using GestionTareasApi.Models;

namespace GestionTareasApi.Controllers
{
    [ApiController]
    [Route("api/tareas")] // Ruta base: http://localhost:5063/api/tareas
    public class TareasController : ControllerBase
    {
        // Almacenamiento temporal en memoria (static para persistencia entre requests)
        private static List<TaskModel> _tareas = new List<TaskModel>();
        private static int _nextId = 1;

        // GET: api/tareas
        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(_tareas);
        }

        // POST: api/tareas
        [HttpPost]
        public IActionResult Crear([FromBody] TaskModel nuevaTarea)
        {
            nuevaTarea.Id = _nextId++;
            _tareas.Add(nuevaTarea);
            // Retorna 201 Created y la ubicación del nuevo recurso
            return CreatedAtAction(nameof(Listar), new { id = nuevaTarea.Id }, nuevaTarea);
        }

        // PUT: api/tareas/{id} -> Corrige el error 405 al recibir el ID por URL
        [HttpPut("{id}")]
        public IActionResult Editar(int id, [FromBody] TaskModel actualizada)
        {
            var tarea = _tareas.FirstOrDefault(t => t.Id == id);
            if (tarea == null) return NotFound(new { mensaje = "Tarea no encontrada" });

            tarea.Title = actualizada.Title;
            tarea.Description = actualizada.Description;
            return Ok(tarea);
        }

        // PATCH: api/tareas/{id}/completar
        [HttpPatch("{id}/completar")]
        public IActionResult Completar(int id)
        {
            var tarea = _tareas.FirstOrDefault(t => t.Id == id);
            if (tarea == null) return NotFound();

            tarea.IsCompleted = true;
            return Ok(tarea);
        }

        // DELETE: api/tareas/{id}
        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            var tarea = _tareas.FirstOrDefault(t => t.Id == id);
            if (tarea == null) return NotFound();

            _tareas.Remove(tarea);
            return NoContent(); // 204 No Content
        }
    }
}