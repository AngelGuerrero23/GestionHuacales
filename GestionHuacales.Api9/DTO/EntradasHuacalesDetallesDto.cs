using System.ComponentModel.DataAnnotations;

namespace GestionHuacales.Api9.DTO;

public class EntradasHuacalesDetallesDto
{

    public int Cantidad { get; set; }
    public double Precio { get; set; }
    public int TipoId { get; set; }
}