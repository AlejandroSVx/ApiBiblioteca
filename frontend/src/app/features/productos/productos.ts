import { HttpErrorResponse } from '@angular/common/http';
import { CurrencyPipe } from '@angular/common';
import { Component, OnInit, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Categoria, Producto, ProductoRequest } from '../../models/inventario';
import { FiltroProductosPipe } from '../../pipes/filtro-productos.pipe';
import { InventarioService } from '../../services/inventario.service';
import { StockBajoDirective } from '../../directives/stock-bajo.directive';

@Component({
  selector: 'app-productos',
  imports: [CurrencyPipe, FormsModule, FiltroProductosPipe, StockBajoDirective],
  templateUrl: './productos.html',
  styleUrl: './productos.css'
})
export class ProductosComponent implements OnInit {
  private readonly inventario = inject(InventarioService);

  protected readonly productos = signal<Producto[]>([]);
  protected readonly categorias = signal<Categoria[]>([]);
  protected readonly cargando = signal(true);
  protected readonly error = signal('');
  protected readonly mensaje = signal('');
  protected busqueda = '';
  protected editandoId: number | null = null;
  protected guardando = false;
  protected formulario: ProductoRequest = this.formularioVacio();

  ngOnInit(): void {
    this.cargarCategorias();
    this.cargarProductos();
  }

  protected guardar(): void {
    this.error.set('');
    this.mensaje.set('');
    this.guardando = true;

    const operacion = this.editandoId === null
      ? this.inventario.crearProducto(this.formulario)
      : this.inventario.actualizarProducto(this.editandoId, this.formulario);

    operacion.subscribe({
      next: () => {
        this.mensaje.set(this.editandoId === null ? 'Producto creado.' : 'Producto actualizado.');
        this.cancelarEdicion();
        this.cargarProductos();
        this.guardando = false;
      },
      error: (error: HttpErrorResponse) => {
        this.error.set(this.mensajeError(error));
        this.guardando = false;
      }
    });
  }

  protected editar(producto: Producto): void {
    this.editandoId = producto.id;
    this.formulario = {
      nombre: producto.nombre,
      descripcion: producto.descripcion,
      precio: producto.precio,
      stock: producto.stock,
      categoriaId: producto.categoriaId
    };
    this.mensaje.set('');
    this.error.set('');
  }

  protected eliminar(producto: Producto): void {
    if (!window.confirm(`¿Eliminar "${producto.nombre}"?`)) {
      return;
    }

    this.inventario.eliminarProducto(producto.id).subscribe({
      next: () => {
        this.mensaje.set('Producto eliminado.');
        this.cargarProductos();
      },
      error: (error: HttpErrorResponse) => this.error.set(this.mensajeError(error))
    });
  }

  protected cancelarEdicion(): void {
    this.editandoId = null;
    this.formulario = this.formularioVacio();
  }

  private cargarProductos(): void {
    this.inventario.obtenerProductos().subscribe({
      next: productos => {
        this.productos.set(productos);
        this.cargando.set(false);
      },
      error: (error: HttpErrorResponse) => {
        this.error.set(this.mensajeError(error));
        this.cargando.set(false);
      }
    });
  }

  private cargarCategorias(): void {
    this.inventario.obtenerCategorias().subscribe({
      next: categorias => this.categorias.set(categorias),
      error: (error: HttpErrorResponse) => this.error.set(this.mensajeError(error))
    });
  }

  private formularioVacio(): ProductoRequest {
    return {
      nombre: '',
      descripcion: '',
      precio: 0.01,
      stock: 0,
      categoriaId: 0
    };
  }

  private mensajeError(error: HttpErrorResponse): string {
    if (error.status === 0) {
      return 'No se pudo conectar con la API. Comprueba que el backend esté ejecutándose.';
    }

    const mensajeServidor = error.error?.mensaje;
    return typeof mensajeServidor === 'string'
      ? mensajeServidor
      : `La solicitud falló (HTTP ${error.status}).`;
  }
}
