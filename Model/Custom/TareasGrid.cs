using Model.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Custom
{
    public class TareasGrid
    {
        public int Id_Tarea { get; set; }
        [Display(Name = "Nombre de la Tarea")]
        public string NomTarea { get; set; }
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
        [Display(Name = "Fecha de Inicio")]
        public DateTime FechInicio { get; set; }
        [Display(Name = "Fecha de Fin")]
        public DateTime FechFin { get; set; }
        [DisplayFormat(DataFormatString = "{0:0.00%}", ApplyFormatInEditMode = false)]
        public double Avance { get; set; }
        [Display(Name = "Titulo del Proyecto")]
        public string TituloProy { get; set; }

        [Display(Name ="Costo Total")]
        public double Costo { get; set; }
    }
}
