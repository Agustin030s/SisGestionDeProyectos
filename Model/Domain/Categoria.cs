using System.ComponentModel.DataAnnotations;

namespace Model.Domain
{
    public class Categoria
    {
        [Key]
        public int Id_Categoria { get; set; }

        [Required(ErrorMessage = "Debe Ingresar un Nombre a la Categoria")]
        [Display(Name = "Nombre de Categoria")]
        [MaxLength(150)]
        public string Nombre { get; set; }
    }
}
