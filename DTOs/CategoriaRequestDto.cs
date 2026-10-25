using System.ComponentModel.DataAnnotations;

namespace ApiBiblioteca.DTOs;

public class CategoriaRequestDto
{
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(80, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 80 caracteres.")]
    public string Nombre { get; set; } = string.Empty;

    [StringLength(250, ErrorMessage = "La descripción no puede superar 250 caracteres.")]
    public string Descripcion { get; set; } = string.Empty;
}
