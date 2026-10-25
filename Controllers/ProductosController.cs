using ApiBiblioteca.DTOs;
using ApiBiblioteca.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiBiblioteca.Controllers;

[ApiController]
[Route("api/v1/productos")]
public class ProductosController : ControllerBase
{
    private readonly IProductoService _productoService;

    public ProductosController(IProductoService productoService)
    {
        _productoService = productoService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductoResponseDto>>> ObtenerTodos()
    {
        return Ok(await _productoService.ObtenerTodosAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductoResponseDto>> BuscarPorId(int id)
    {
        var producto = await _productoService.BuscarPorIdAsync(id);
        return producto is null ? NotFound() : Ok(producto);
    }

    [HttpPost]
    public async Task<ActionResult<ProductoResponseDto>> Crear(ProductoRequestDto request)
    {
        if (!await _productoService.ExisteCategoriaAsync(request.CategoriaId))
            return BadRequest(new { mensaje = "La categoría indicada no existe." });

        var producto = await _productoService.CrearAsync(request);
        return CreatedAtAction(nameof(BuscarPorId), new { id = producto.Id }, producto);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ProductoResponseDto>> Actualizar(int id, ProductoRequestDto request)
    {
        if (!await _productoService.ExisteCategoriaAsync(request.CategoriaId))
            return BadRequest(new { mensaje = "La categoría indicada no existe." });

        var producto = await _productoService.ActualizarAsync(id, request);
        return producto is null ? NotFound() : Ok(producto);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        return await _productoService.EliminarAsync(id) ? NoContent() : NotFound();
    }
}
