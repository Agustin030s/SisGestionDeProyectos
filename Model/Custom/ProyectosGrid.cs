using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Custom
{
    public class ProyectosGrid
    {
        public int Id_Proyecto { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        [Display(Name = "Fecha de Inicio")]
        public DateTime FechInicio { get; set; }
        [Display(Name = "Fecha de Fin")]
        public DateTime FechFin { get; set; }
        [DisplayFormat(DataFormatString = "{0:0.00%}", ApplyFormatInEditMode = false)]
        public double Avance { get; set; }
        public string Encargado { get; set; }
        public string Categoria { get; set; }


        [Display(Name = "Costo Total")]
        public double Costo { get; set; }
    }
}
