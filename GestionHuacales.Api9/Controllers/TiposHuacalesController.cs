using GestionHuacales.Api9.DTO;
using GestionHuacales.Api9.Models;
using GestionHuacales.Api9.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestionHuacales.Api9.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TiposHuacalesController (HuacalesService huacalesService): ControllerBase
{
    // GET: api/<TiposHuacalesController>
    [HttpGet]
    public async Task<TipoHuacalesDto[]> Get()
    {
        return await huacalesService.ListarTipo();
    }

    // GET api/<TiposHuacalesController>/5
    [HttpGet("{id}")]
    public async Task<TipoHuacalesDto[]>Get(int id)
    {
        return await huacalesService.ListarConId(h => h.TipoId == id);
    }

    //// POST api/<TiposHuacalesController>
    //[HttpPost]
    //public void Post([FromBody] string value)
    //{
    //}

    //// PUT api/<TiposHuacalesController>/5
    //[HttpPut("{id}")]
    //public void Put(int id, [FromBody] string value)
    //{
    //}

    //// DELETE api/<TiposHuacalesController>/5
    //[HttpDelete("{id}")]
    //public void Delete(int id)
    //{
    //}
}
