using ApiBiblioteca.DTOs;

namespace ApiBiblioteca.Services;

public interface ICategoriaService
{
    Task<List<CategoriaResponseDto>> ObtenerTodasAsync();
    Task<CategoriaResponseDto?> BuscarPorIdAsync(int id);
    Task<CategoriaResponseDto> CrearAsync(CategoriaRequestDto request);
    Task<CategoriaResponseDto?> ActualizarAsync(int id, CategoriaRequestDto request);
    Task<bool> EliminarAsync(int id);
}
