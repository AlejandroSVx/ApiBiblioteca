# Frontend de inventario

Aplicación Angular 22 para administrar productos y categorías mediante la API ASP.NET Core.

## Requisitos

- Node.js 24 o compatible con Angular 22
- API ejecutándose en `http://localhost:5134`

## Ejecución

Desde esta carpeta:

```powershell
npm install
npm start
```

Abre `http://localhost:4200`. El backend se inicia desde la raíz del repositorio con `dotnet run`.

## Funciones

- Crear, consultar, actualizar y eliminar productos.
- Crear, consultar, actualizar y eliminar categorías.
- Buscar productos por nombre, descripción o categoría.
- Mostrar visualmente productos con cinco unidades o menos.
