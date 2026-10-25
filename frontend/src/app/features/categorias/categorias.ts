import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Categoria, CategoriaRequest } from '../../models/inventario';
import { InventarioService } from '../../services/inventario.service';

@Component({
  selector: 'app-categorias',
  imports: [FormsModule],
  templateUrl: './categorias.html',
  styleUrl: './categorias.css'
})
export class CategoriasComponent implements OnInit {
  private readonly inventario = inject(InventarioService);

  protected readonly categorias = signal<Categoria[]>([]);
  protected readonly cargando = signal(true);
  protected readonly error = signal('');
  protected readonly mensaje = signal('');
  protected editandoId: number | null = null;
  protected guardando = false;
  protected formulario: CategoriaRequest = this.formularioVacio();

  ngOnInit(): void {
    this.cargarCategorias();
  }

  protected guardar(): void {
    this.error.set('');
    this.mensaje.set('');
    this.guardando = true;

    const operacion = this.editandoId === null
      ? this.inventario.crearCategoria(this.formulario)
      : this.inventario.actualizarCategoria(this.editandoId, this.formulario);

    operacion.subscribe({
      next: () => {
        this.mensaje.set(this.editandoId === null ? 'Categoría creada.' : 'Categoría actualizada.');
        this.cancelarEdicion();
        this.cargarCategorias();
        this.guardando = false;
      },
      error: (error: HttpErrorResponse) => {
        this.error.set(this.mensajeError(error));
        this.guardando = false;
      }
    });
  }

  protected editar(categoria: Categoria): void {
    this.editandoId = categoria.id;
    this.formulario = {
      nombre: categoria.nombre,
      descripcion: categoria.descripcion
    };
    this.mensaje.set('');
    this.error.set('');
  }

  protected eliminar(categoria: Categoria): void {
    if (!window.confirm(`¿Eliminar la categoría "${categoria.nombre}"?`)) {
      return;
    }

    this.inventario.eliminarCategoria(categoria.id).subscribe({
      next: () => {
        this.mensaje.set('Categoría eliminada.');
        this.cargarCategorias();
      },
      error: (error: HttpErrorResponse) => this.error.set(this.mensajeError(error))
    });
  }

  protected cancelarEdicion(): void {
    this.editandoId = null;
    this.formulario = this.formularioVacio();
  }

  private cargarCategorias(): void {
    this.inventario.obtenerCategorias().subscribe({
      next: categorias => {
        this.categorias.set(categorias);
        this.cargando.set(false);
      },
      error: (error: HttpErrorResponse) => {
        this.error.set(this.mensajeError(error));
        this.cargando.set(false);
      }
    });
  }

  private formularioVacio(): CategoriaRequest {
    return {
      nombre: '',
      descripcion: ''
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
