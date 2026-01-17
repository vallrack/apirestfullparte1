namespace GestionTareasApi.Models
{
    /// <summary>
    /// Representa la entidad de una tarea en el sistema.
    /// </summary>
    public class TaskModel
    {
        // Identificador único de la tarea
        public int Id { get; set; }

        // Título descriptivo (inicializado para evitar advertencias de nulabilidad)
        public string Title { get; set; } = string.Empty;

        // Detalle de la actividad
        public string Description { get; set; } = string.Empty;

        // Estado de cumplimiento (por defecto: pendiente)
        public bool IsCompleted { get; set; } = false;
    }
}