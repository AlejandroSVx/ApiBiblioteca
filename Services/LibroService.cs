using ApiBiblioteca.Data;
using ApiBiblioteca.DTOs;
using ApiBiblioteca.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiBiblioteca.Services;

public class LibroService : ILibroService
{
    private readonly AppDbContext _context;

    public LibroService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<LibroResponseDto>> ObtenerTodosAsync()
    {
        return await _context.Libros
            .Select(libro => new LibroResponseDto
            {
                Id = libro.Id,
                Titulo = libro.Titulo,
                Autor = libro.Autor,
                Genero = libro.Genero,
                AnioPublicacion = libro.AnioPublicacion,
                Disponible = libro.Disponible
            })
            .ToListAsync();
    }

    public async Task<LibroResponseDto?> BuscarPorIdAsync(int id)
    {
        return await _context.Libros
            .Where(libro => libro.Id == id)
            .Select(libro => new LibroResponseDto
            {
                Id = libro.Id,
                Titulo = libro.Titulo,
                Autor = libro.Autor,
                Genero = libro.Genero,
                AnioPublicacion = libro.AnioPublicacion,
                Disponible = libro.Disponible
            })
            .FirstOrDefaultAsync();
    }

    public async Task<LibroResponseDto> CrearAsync(LibroRequestDto libroRequest)
    {
        var libro = new Libro
        {
            Titulo = libroRequest.Titulo,
            Autor = libroRequest.Autor,
            Genero = libroRequest.Genero,
            AnioPublicacion = libroRequest.AnioPublicacion,
            Disponible = libroRequest.Disponible
        };

        _context.Libros.Add(libro);
        await _context.SaveChangesAsync();

        return new LibroResponseDto
        {
            Id = libro.Id,
            Titulo = libro.Titulo,
            Autor = libro.Autor,
            Genero = libro.Genero,
            AnioPublicacion = libro.AnioPublicacion,
            Disponible = libro.Disponible
        };
    }

    public async Task<LibroResponseDto?> ActualizarAsync(int id, LibroRequestDto libroRequest)
    {
        var libro = await _context.Libros.FindAsync(id);
        if (libro is null)
            return null;

        libro.Titulo = libroRequest.Titulo;
        libro.Autor = libroRequest.Autor;
        libro.Genero = libroRequest.Genero;
        libro.AnioPublicacion = libroRequest.AnioPublicacion;
        libro.Disponible = libroRequest.Disponible;

        await _context.SaveChangesAsync();

        return new LibroResponseDto
        {
            Id = libro.Id,
            Titulo = libro.Titulo,
            Autor = libro.Autor,
            Genero = libro.Genero,
            AnioPublicacion = libro.AnioPublicacion,
            Disponible = libro.Disponible
        };
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var libro = await _context.Libros.FindAsync(id);
        if (libro is null)
            return false;

        _context.Libros.Remove(libro);
        await _context.SaveChangesAsync();
        return true;
    }
}
