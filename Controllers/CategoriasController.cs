using ApiBiblioteca.DTOs;
using ApiBiblioteca.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiBiblioteca.Controllers;

[ApiController]
[Route("api/v1/categorias")]
public class CategoriasController : ControllerBase
{
    private readonly ICategoriaService _categoriaService;

    public CategoriasController(ICategoriaService categoriaService)
    {
        _categoriaService = categoriaService;
    }

    [HttpGet]
    public async Task<ActionResult<List<CategoriaResponseDto>>> ObtenerTodas()
    {
        return Ok(await _categoriaService.ObtenerTodasAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CategoriaResponseDto>> BuscarPorId(int id)
    {
        var categoria = await _categoriaService.BuscarPorIdAsync(id);
        return categoria is null ? NotFound() : Ok(categoria);
    }

    [HttpPost]
    public async Task<ActionResult<CategoriaResponseDto>> Crear(CategoriaRequestDto request)
    {
        var categoria = await _categoriaService.CrearAsync(request);
        return CreatedAtAction(nameof(BuscarPorId), new { id = categoria.Id }, categoria);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<CategoriaResponseDto>> Actualizar(int id, CategoriaRequestDto request)
    {
        var categoria = await _categoriaService.ActualizarAsync(id, request);
        return categoria is null ? NotFound() : Ok(categoria);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        if (await _categoriaService.BuscarPorIdAsync(id) is null)
            return NotFound();

        if (!await _categoriaService.EliminarAsync(id))
            return Conflict(new { mensaje = "No se puede eliminar una categoría que tiene productos asociados." });

        return NoContent();
    }
}
