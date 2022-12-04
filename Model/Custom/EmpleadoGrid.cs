using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Custom
{
    public class EmpleadoGrid
    {
        public int Legajo { get; set; }
        public int DNI { get; set; }
        [Display(Name = "Nombre Completo")]
        public string ApyNom { get; set; }
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime FechNacimiento { get; set; }
        public string Usuario { get; set; }
        public string Rol { get; set; }
    }
}
