using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Custom
{
    public class RecursoGrid
    {
        public int Id_Recurso { get; set; }
        public double Presupuesto { get; set; }
        [Display(Name ="Nombre Completo")]
        public string ApyNom { get; set; }
        public string Rol { get; set; }
        public int IdTarea { get; set; }
        [Display(Name = "Nombre de la Tarea")]
        public string NomTarea { get; set; }

        [Display(Name = "Fecha de Inicio")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechInicio { get; set; }

        [Display(Name = "Fecha de Fin")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechFin { get; set; }
    }
}
