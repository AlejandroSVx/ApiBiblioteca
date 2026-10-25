import { Pipe, PipeTransform } from '@angular/core';
import { Producto } from '../models/inventario';

@Pipe({
  name: 'filtroProductos',
  standalone: true
})
export class FiltroProductosPipe implements PipeTransform {
  transform(productos: Producto[], busqueda: string): Producto[] {
    const termino = busqueda.trim().toLocaleLowerCase();
    if (!termino) {
      return productos;
    }

    return productos.filter(producto =>
      `${producto.nombre} ${producto.descripcion} ${producto.categoriaNombre}`
        .toLocaleLowerCase()
        .includes(termino)
    );
  }
}
