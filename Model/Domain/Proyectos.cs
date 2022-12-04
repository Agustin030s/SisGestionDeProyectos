using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Domain
{
    public class Proyectos
    {
        [Key]
        public int Id_Proyecto { get; set; }

        [Required(ErrorMessage = "Debe ingresar un titulo al Proyecto")]
        [MaxLength(180)]
        public string Titulo { get; set; }
        [Display(Name = "Descripción del Proyecto")]
        [DataType(DataType.MultilineText)]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Debe Ingresar la Fecha de Inicio del Proyecto")]
        [Display(Name = "Fecha de Inicio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechInicio { get; set; }

        [Required(ErrorMessage = "Debe Ingresar la Fecha de Fin del Proyecto")]
        [Display(Name = "Fecha de Fin")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechFin { get; set; }
        [DisplayFormat(DataFormatString = "{0:0.00%}", ApplyFormatInEditMode = false)]
        public double Avance { get; set; }
        
        [Display(Name = "Encargado")]
        public int Legajo { get; set; }

        [ForeignKey("Legajo")]
        public virtual Empleado Empleado { get; set; }

        [Display(Name = "Categoria del Proyecto")]
        public int Id_Categoria { get; set; }

        [ForeignKey("Id_Categoria")]
        public virtual Categoria Categoria { get; set; }

        public double Costo { get; set; }
    }
}
