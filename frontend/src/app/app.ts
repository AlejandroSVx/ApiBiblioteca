import { Component, signal } from '@angular/core';
import { CategoriasComponent } from './features/categorias/categorias';
import { ProductosComponent } from './features/productos/productos';

@Component({
  imports: [CategoriasComponent, ProductosComponent],
  selector: 'app-root',
  styleUrl: './app.css',
  templateUrl: './app.html'
})
export class App {
  protected readonly seccion = signal<'productos' | 'categorias'>('productos');
}
