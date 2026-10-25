using ApiBiblioteca.DTOs;
using ApiBiblioteca.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiBiblioteca.Controllers;

[ApiController]
[Route("api/v1/libros")]
public class LibrosController : ControllerBase
{
    private readonly ILibroService _libroService;

    public LibrosController(ILibroService libroService)
    {
        _libroService = libroService;
    }

    [HttpGet]
    public async Task<ActionResult<List<LibroResponseDto>>> ObtenerTodos()
    {
        return Ok(await _libroService.ObtenerTodosAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<LibroResponseDto>> BuscarPorId(int id)
    {
        var libro = await _libroService.BuscarPorIdAsync(id);
        if (libro is null)
            return NotFound();

        return Ok(libro);
    }

    [HttpPost]
    public async Task<ActionResult<LibroResponseDto>> Crear(LibroRequestDto libroRequest)
    {
        var libro = await _libroService.CrearAsync(libroRequest);
        return CreatedAtAction(nameof(BuscarPorId), new { id = libro.Id }, libro);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<LibroResponseDto>> Actualizar(int id, LibroRequestDto libroRequest)
    {
        var libro = await _libroService.ActualizarAsync(id, libroRequest);
        if (libro is null)
            return NotFound();

        return Ok(libro);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        var eliminado = await _libroService.EliminarAsync(id);
        if (!eliminado)
            return NotFound();

        return NoContent();
    }
}
