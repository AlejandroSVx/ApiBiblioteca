using ApiBiblioteca.Data;
using ApiBiblioteca.DTOs;
using ApiBiblioteca.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiBiblioteca.Services;

public class CategoriaService : ICategoriaService
{
    private readonly AppDbContext _context;

    public CategoriaService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<CategoriaResponseDto>> ObtenerTodasAsync()
    {
        return await _context.Categorias
            .OrderBy(categoria => categoria.Nombre)
            .Select(categoria => new CategoriaResponseDto
            {
                Id = categoria.Id,
                Nombre = categoria.Nombre,
                Descripcion = categoria.Descripcion
            })
            .ToListAsync();
    }

    public async Task<CategoriaResponseDto?> BuscarPorIdAsync(int id)
    {
        return await _context.Categorias
            .Where(categoria => categoria.Id == id)
            .Select(categoria => new CategoriaResponseDto
            {
                Id = categoria.Id,
                Nombre = categoria.Nombre,
                Descripcion = categoria.Descripcion
            })
            .FirstOrDefaultAsync();
    }

    public async Task<CategoriaResponseDto> CrearAsync(CategoriaRequestDto request)
    {
        var categoria = new Categoria
        {
            Nombre = request.Nombre,
            Descripcion = request.Descripcion
        };

        _context.Categorias.Add(categoria);
        await _context.SaveChangesAsync();

        return new CategoriaResponseDto
        {
            Id = categoria.Id,
            Nombre = categoria.Nombre,
            Descripcion = categoria.Descripcion
        };
    }

    public async Task<CategoriaResponseDto?> ActualizarAsync(int id, CategoriaRequestDto request)
    {
        var categoria = await _context.Categorias.FindAsync(id);
        if (categoria is null)
            return null;

        categoria.Nombre = request.Nombre;
        categoria.Descripcion = request.Descripcion;
        await _context.SaveChangesAsync();

        return new CategoriaResponseDto
        {
            Id = categoria.Id,
            Nombre = categoria.Nombre,
            Descripcion = categoria.Descripcion
        };
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var categoria = await _context.Categorias.FindAsync(id);
        if (categoria is null || await _context.Productos.AnyAsync(producto => producto.CategoriaId == id))
            return false;

        _context.Categorias.Remove(categoria);
        await _context.SaveChangesAsync();
        return true;
    }
}
