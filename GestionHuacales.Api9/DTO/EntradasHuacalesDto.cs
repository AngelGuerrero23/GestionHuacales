namespace GestionHuacales.Api9.DTO;

public class EntradasHuacalesDto
{
    public string NombreCliente { get; set; }
    public EntradasHuacalesDetallesDto[] Huacales { get; set; } = [];
}


