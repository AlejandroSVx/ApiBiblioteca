using ApiBiblioteca.Data;
using ApiBiblioteca.DTOs;
using ApiBiblioteca.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiBiblioteca.Services;

public class ProductoService : IProductoService
{
    private readonly AppDbContext _context;

    public ProductoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProductoResponseDto>> ObtenerTodosAsync()
    {
        return await ConsultarProductos()
            .OrderBy(producto => producto.Nombre)
            .ToListAsync();
    }

    public async Task<ProductoResponseDto?> BuscarPorIdAsync(int id)
    {
        return await ConsultarProductos().FirstOrDefaultAsync(producto => producto.Id == id);
    }

    public async Task<ProductoResponseDto> CrearAsync(ProductoRequestDto request)
    {
        var producto = new Producto
        {
            Nombre = request.Nombre,
            Descripcion = request.Descripcion,
            Precio = request.Precio,
            Stock = request.Stock,
            CategoriaId = request.CategoriaId
        };

        _context.Productos.Add(producto);
        await _context.SaveChangesAsync();
        return await ConsultarProductos().SingleAsync(resultado => resultado.Id == producto.Id);
    }

    public async Task<ProductoResponseDto?> ActualizarAsync(int id, ProductoRequestDto request)
    {
        var producto = await _context.Productos.FindAsync(id);
        if (producto is null)
            return null;

        producto.Nombre = request.Nombre;
        producto.Descripcion = request.Descripcion;
        producto.Precio = request.Precio;
        producto.Stock = request.Stock;
        producto.CategoriaId = request.CategoriaId;
        await _context.SaveChangesAsync();

        return await BuscarPorIdAsync(producto.Id);
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var producto = await _context.Productos.FindAsync(id);
        if (producto is null)
            return false;

        _context.Productos.Remove(producto);
        await _context.SaveChangesAsync();
        return true;
    }

    public Task<bool> ExisteCategoriaAsync(int categoriaId)
    {
        return _context.Categorias.AnyAsync(categoria => categoria.Id == categoriaId);
    }

    private IQueryable<ProductoResponseDto> ConsultarProductos()
    {
        return _context.Productos.Select(producto => new ProductoResponseDto
        {
            Id = producto.Id,
            Nombre = producto.Nombre,
            Descripcion = producto.Descripcion,
            Precio = producto.Precio,
            Stock = producto.Stock,
            CategoriaId = producto.CategoriaId,
            CategoriaNombre = producto.Categoria.Nombre
        });
    }
}
