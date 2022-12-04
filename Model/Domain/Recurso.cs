using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Domain
{
    public class Recurso
    {
        [Key]
        public int Id_Recurso { get; set; }
        public double Presupuesto { get; set; }

        [Display(Name ="Empleado")]
        public int Legajo { get; set; }

        [ForeignKey("Legajo")]
        public virtual Empleado Empleado { get; set; }

        public int Id_Tarea { get; set; }
        [ForeignKey("Id_Tarea")]
        public virtual Tareas Tarea { get; set; }

    }
}
