using ApiBiblioteca.DTOs;

namespace ApiBiblioteca.Services;

public interface ILibroService
{
    Task<List<LibroResponseDto>> ObtenerTodosAsync();
    Task<LibroResponseDto?> BuscarPorIdAsync(int id);
    Task<LibroResponseDto> CrearAsync(LibroRequestDto libroRequest);
    Task<LibroResponseDto?> ActualizarAsync(int id, LibroRequestDto libroRequest);
    Task<bool> EliminarAsync(int id);
}
