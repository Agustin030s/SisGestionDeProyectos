using System.ComponentModel.DataAnnotations;

namespace Model.Domain
{
    public class RolEmp
    {
        [Key]
        public int Id_Rol { get; set; }

        [Required(ErrorMessage = "Debe ingresar un nombre al Rol")]
        [Display(Name ="Nombre del Rol")]
        [MaxLength(120)]
        public string Nombre { get; set; }

        [Required(ErrorMessage ="Debe ingresar el precio por el servicio")]
        [Display(Name = "Precio del Servicio x Dia")]
        public double PrecioServicio { get; set; }
    }
}
