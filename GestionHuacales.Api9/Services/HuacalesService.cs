using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using GestionHuacales.Api9.DAL;
using GestionHuacales.Api9.Models;
using GestionHuacales.Api9.DTO;

namespace GestionHuacales.Api9.Services;

public class HuacalesService(IDbContextFactory<Contexto>DbFactory)
{

    public async Task<bool> Guardar(EntradasHuacales huacales)
    {
        if (!await Existe(huacales.IdEntrada))
        {
            return await Insertar(huacales);
        }
        else
        {
            return await Modificar(huacales);
        }
       
    }

    private async Task AfectarExistencia(EntradasHuacalesDetalles[] detalles, TipoOperacion tipoOperacion)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        foreach (var detalle in detalles)
        {
            var huacal = await contexto.TiposHuacales.FindAsync(detalle.TipoId);
            if (tipoOperacion == TipoOperacion.Resta)
                huacal.Existencia -= detalle.Cantidad;
            else
                huacal.Existencia += detalle.Cantidad;
            
        }
        await contexto.SaveChangesAsync();
    }
    public async Task<bool>Modificar(EntradasHuacales huacales)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var huacal = await contexto.EntradasHuacales
            .Include(e => e.EntradasHuacalesDetalles)
            .AsNoTracking()
            .SingleOrDefaultAsync(e => e.IdEntrada == huacales.IdEntrada);
        if (huacal == null) return false;

        await AfectarExistencia(huacal.EntradasHuacalesDetalles.ToArray(),
            TipoOperacion.Resta);

        contexto.EntradasHuacalesDetalles.RemoveRange(huacal.EntradasHuacalesDetalles);
        contexto.Update(huacales);

        await AfectarExistencia(huacales.EntradasHuacalesDetalles.ToArray(),
           TipoOperacion.Suma);

        return await contexto.SaveChangesAsync()>0;
    }
    public async Task<bool> Insertar(EntradasHuacales huacales)
     {
         await using var contexto = await DbFactory.CreateDbContextAsync();
         contexto.EntradasHuacales.Add(huacales);
         await AfectarExistencia(huacales.EntradasHuacalesDetalles.ToArray(), TipoOperacion.Suma);
         return await contexto.SaveChangesAsync() > 0;

     }
    public async Task<bool>Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var huacal = await contexto.EntradasHuacales
            .Include(o=>o.EntradasHuacalesDetalles)
            .FirstOrDefaultAsync(c => c.IdEntrada == id);
        
        if(huacal == null) return false;

        await AfectarExistencia(huacal.EntradasHuacalesDetalles.ToArray(), TipoOperacion.Resta);

        contexto.EntradasHuacalesDetalles.RemoveRange(huacal.EntradasHuacalesDetalles);
        contexto.EntradasHuacales.Remove(huacal);
        var cantidad = await contexto.SaveChangesAsync();
        return cantidad > 0;
    }

    public async Task<EntradasHuacales?>Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.EntradasHuacales
            .Include(d=>d.EntradasHuacalesDetalles).FirstOrDefaultAsync(o=>o.IdEntrada == id);
    
    }

    public async Task<bool>Existe(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.EntradasHuacales
            .AnyAsync(o=>o.IdEntrada==id);
    }

    public async Task<EntradasHuacalesDto[]> Listar(Expression<Func<EntradasHuacales, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.EntradasHuacales
            .Where(criterio)
            .Select(h => new EntradasHuacalesDto
            {
                NombreCliente = h.NombreCliente
            }).ToArrayAsync();            
    }
  
    public async Task<TipoHuacalesDto[]>ListarTipo()
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.TiposHuacales
            .Where(h => h.TipoId > 0).Select(h => new TipoHuacalesDto
            {
                Descripcion = h.Descripcion,
                Existencia = h.Existencia,
                TipoId = h.TipoId
            }).ToArrayAsync();
    }

    public async Task<TipoHuacalesDto[]>ListarConId(Expression<Func<TiposHuacales, bool> >criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.TiposHuacales
            .Where(criterio).Select(h => new TipoHuacalesDto
            {
                Descripcion = h.Descripcion,
                Existencia = h.Existencia,
                TipoId = h.TipoId
            }).ToArrayAsync();
    }
}

public enum TipoOperacion
    {
        Suma =1,
        Resta =2
    }



