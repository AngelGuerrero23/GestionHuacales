using System.ComponentModel.DataAnnotations;

namespace GestionHuacales.Api9.Models;

public class TiposHuacales
{
    [Key]
    public int TipoId { get; set; }
    [Required(ErrorMessage ="El campo es requerido")]
    public string Descripcion { get; set; }

    [Required]
    [Range(0, int.MaxValue,ErrorMessage ="No puede ser menor a 0")]
    public int Existencia { get; set; }

}
