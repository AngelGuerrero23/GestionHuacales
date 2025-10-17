using System.ComponentModel.DataAnnotations;

namespace GestionHuacales.Api9.DTO;

public class TipoHuacalesDto
{
    public int TipoId { get; set; }
    public string Descripcion { get; set; }

    public int Existencia { get; set; }
}
