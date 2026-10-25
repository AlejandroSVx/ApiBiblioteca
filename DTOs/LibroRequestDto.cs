using System.ComponentModel.DataAnnotations;

namespace ApiBiblioteca.DTOs;

public class LibroRequestDto
{
    [Required(ErrorMessage = "El titulo es obligatorio.")]
    [StringLength(120, MinimumLength = 2, ErrorMessage = "El titulo debe tener entre 2 y 120 caracteres.")]
    public string Titulo { get; set; } = string.Empty;

    [Required(ErrorMessage = "El autor es obligatorio.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "El autor debe tener entre 2 y 100 caracteres.")]
    public string Autor { get; set; } = string.Empty;

    [Required(ErrorMessage = "El genero es obligatorio.")]
    [StringLength(60, MinimumLength = 2, ErrorMessage = "El genero debe tener entre 2 y 60 caracteres.")]
    public string Genero { get; set; } = string.Empty;

    [Range(1, 2100, ErrorMessage = "El anio de publicacion debe ser valido.")]
    public int AnioPublicacion { get; set; }

    public bool Disponible { get; set; } = true;
}
