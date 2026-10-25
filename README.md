# API de productos y categorías

API REST académica desarrollada con ASP.NET Core 10, Entity Framework Core y SQLite. Está organizada en controladores, servicios, DTOs y modelos. Cada producto pertenece a una categoría; no se permite eliminar una categoría que tenga productos asociados.

## Requisitos y ejecución

- .NET SDK 10
- Node.js compatible con Angular 22
- Inicia la API desde la raíz del repositorio con `dotnet run`
- La aplicación crea `ApiBiblioteca.db` al iniciar y muestra en la terminal la dirección local.
- En entorno Development, la especificación OpenAPI está disponible en `/openapi/v1.json`.
- En otra terminal ejecuta `cd frontend`, `npm install` y `npm start`. Abre `http://localhost:4200`.
- El frontend Angular usa componentes separados para productos y categorías, un servicio HTTP, un pipe de búsqueda y una directiva visual para indicar stock bajo.
- La API permite solicitudes CORS desde `http://localhost:4200` y `http://127.0.0.1:4200`.

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

`ApiProductos.postman_collection.json` contiene las solicitudes HTTP de productos y categorías en formato de colección Postman v2.1 para importarla en la extensión Flashpost.

## Estructura

- `Controllers`, `Services`, `Models`, `DTOs` y `Data`: API ASP.NET Core.
- `frontend`: aplicación Angular 22.
