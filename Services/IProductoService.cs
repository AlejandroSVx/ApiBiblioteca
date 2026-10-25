using ApiBiblioteca.DTOs;

namespace ApiBiblioteca.Services;

public interface IProductoService
{
    Task<List<ProductoResponseDto>> ObtenerTodosAsync();
    Task<ProductoResponseDto?> BuscarPorIdAsync(int id);
    Task<ProductoResponseDto> CrearAsync(ProductoRequestDto request);
    Task<ProductoResponseDto?> ActualizarAsync(int id, ProductoRequestDto request);
    Task<bool> EliminarAsync(int id);
    Task<bool> ExisteCategoriaAsync(int categoriaId);
}
