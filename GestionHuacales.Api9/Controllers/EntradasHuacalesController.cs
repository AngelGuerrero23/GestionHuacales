using GestionHuacales.Api9.DTO;
using GestionHuacales.Api9.Models;
using GestionHuacales.Api9.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestionHuacales.Api9.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EntradasHuacalesController(HuacalesService huacalesService) : ControllerBase
{
    // GET: api/<EntradasHuacalesController>
    [HttpGet]
    public async Task<EntradasHuacalesDto[]> Get()
    {
        return await huacalesService.Listar(h => true);
    }

    // GET api/<EntradasHuacalesController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<EntradasHuacalesController>
    [HttpPost]
    public async Task Post([FromBody] EntradasHuacalesDto entradaHuacal)
    {
        var newHuacal = new EntradasHuacales
        {
            NombreCliente = entradaHuacal.NombreCliente,
            Fecha = DateTime.UtcNow,
            Cantidad = 2,
            EntradasHuacalesDetalles = entradaHuacal.Huacales.Select(h => new EntradasHuacalesDetalles
            {
                TipoId = h.TipoId,
                Cantidad = h.Cantidad,
                Precio = h.Precio
            }).ToArray()

        };
        await huacalesService.Guardar(newHuacal);
    }

    // PUT api/<EntradasHuacalesController>/5
    [HttpPut("{id}")]
    public async Task Put(int id, [FromBody] EntradasHuacalesDto entradaHuacal)
    {
        var newHuacal = new EntradasHuacales
        {
            IdEntrada = id,
            NombreCliente = entradaHuacal.NombreCliente,
            Fecha = DateTime.UtcNow,
            Cantidad = 2,
            EntradasHuacalesDetalles = entradaHuacal.Huacales.Select(h => new EntradasHuacalesDetalles
            {
                TipoId = id,
                Cantidad = h.Cantidad,
                Precio = h.Precio
            }).ToList(),

        }; ;
        await huacalesService.Guardar(newHuacal);
    }

    // DELETE api/<EntradasHuacalesController>/5
    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        await huacalesService.Eliminar(id);
    }
}
