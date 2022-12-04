using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Domain
{
    public class Tareas
    {
        [Key]
        public int Id_Tarea { get; set; }

        [Required(ErrorMessage = "Debe ingresar un Nombre para la Tarea")]
        [Display(Name ="Nombre de la Tarea")]
        [MaxLength(256)]
        public string NomTarea { get; set; }

        [Display(Name = "Descripción")]
        [DataType(DataType.MultilineText)]
        [MaxLength(256)]
        public string Descripcion { get; set; }

        [Required(ErrorMessage ="Debe ingresar una Fecha de Inicio")]
        [Display(Name = "Fecha de Inicio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechInicio { get; set; }

        [Required(ErrorMessage = "Debe ingresar una Fecha de Fin")]
        [Display(Name = "Fecha de Fin")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechFin { get; set; }
        [DisplayFormat(DataFormatString = "{0:0.00%}", ApplyFormatInEditMode = false)]
        public double Avance { get; set; }

        [Display(Name = "Proyecto")]
        public int Id_Proyecto { get; set; }

        [ForeignKey("Id_Proyecto")]
        public virtual Proyectos Proyecto { get; set; }

        public double Costo { get; set; }

    }
}
