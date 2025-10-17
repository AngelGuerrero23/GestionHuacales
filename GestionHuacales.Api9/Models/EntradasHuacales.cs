using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionHuacales.Api9.Models;

public class EntradasHuacales
{
    [Key]
    public int IdEntrada { get; set; }
    [Required(ErrorMessage="La fecha es obligatoria")]
    public DateTime Fecha {  get; set; }
    [Required(ErrorMessage ="El campo es obligatorio")]
    public string NombreCliente { get; set; }
    [Required]
    [Range(0,int.MaxValue, ErrorMessage ="Debe ser mayor a 0")]
    public int Cantidad { get; set; }
    [Required]
    [Range(0, int.MaxValue, ErrorMessage ="Debe digitar un monto positivo")]
    public double Precio { get; set; }

    [ForeignKey("IdEntrada")]
    //[InverseProperty("EntradaHuacal")]
    public virtual ICollection<EntradasHuacalesDetalles> EntradasHuacalesDetalles { get; set; } = new List<EntradasHuacalesDetalles>();

}


