using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Domain
{
    public class UserPorEmp
    {
        [Key]
        public int IdUserxEmp { get; set; }
        public int Legajo { get; set; }
        [ForeignKey("Legajo")]
        public virtual Empleado Empleado { get; set; }

        public string IdUsuario { get; set; }
        [ForeignKey("IdUsuario")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
