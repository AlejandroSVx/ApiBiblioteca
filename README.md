# API Biblioteca

API REST para gestionar libros. Proyecto academico organizado por capas con ASP.NET Core, Entity Framework Core, SQLite y DTOs.

## Ejecutar

Ejecuta dotnet run desde la carpeta del proyecto. La aplicacion crea la base local ApiBiblioteca.db si todavia no existe. Usa la URL que indique la terminal.

## Rutas

- GET /api/v1/libros — listar libros
- GET /api/v1/libros/{id} — buscar por ID
- POST /api/v1/libros — crear
- PUT /api/v1/libros/{id} — actualizar
- DELETE /api/v1/libros/{id} — eliminar

Los cuerpos de POST y PUT usan LibroRequestDto. Las respuestas usan LibroResponseDto.
