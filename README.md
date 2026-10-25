# API de productos y categorías

API REST académica desarrollada con ASP.NET Core 10, Entity Framework Core y SQLite. Está organizada en controladores, servicios, DTOs y modelos. Cada producto pertenece a una categoría; no se permite eliminar una categoría que tenga productos asociados.

## Requisitos y ejecución

- .NET SDK 10
- Desde la raíz del proyecto: `dotnet run`
- La aplicación crea `ApiBiblioteca.db` al iniciar y muestra en la terminal la dirección local.
- En entorno Development, la especificación OpenAPI está disponible en `/openapi/v1.json`.

## Endpoints

Todas las rutas usan el prefijo `/api/v1`.

| Método | Ruta | Acción |
|---|---|---|
| GET | `/categorias` | Listar categorías |
| GET | `/categorias/{id}` | Consultar categoría |
| POST | `/categorias` | Crear categoría |
| PUT | `/categorias/{id}` | Actualizar categoría |
| DELETE | `/categorias/{id}` | Eliminar categoría sin productos asociados |
| GET | `/productos` | Listar productos |
| GET | `/productos/{id}` | Consultar producto |
| POST | `/productos` | Crear producto |
| PUT | `/productos/{id}` | Actualizar producto |
| DELETE | `/productos/{id}` | Eliminar producto |

Los datos de entrada se validan mediante DTOs. Para crear o actualizar un producto, `categoriaId` debe corresponder a una categoría existente.

## Pruebas HTTP y colección

`ApiBiblioteca.http` contiene solicitudes de ejemplo que se pueden ejecutar desde Visual Studio Code. Crear primero una categoría y usar el ID que devuelve al probar los endpoints de productos.

`ApiProductos.postman_collection.json` es una colección JSON compatible con Postman Collection v2.1. Se puede importar en Flashpost si la herramienta admite ese formato y luego exportar/guardar desde Flashpost el JSON requerido por la entrega.
