import { Directive, HostBinding, Input } from '@angular/core';

@Directive({
  selector: '[appStockBajo]',
  standalone: true
})
export class StockBajoDirective {
  @Input({ required: true }) appStockBajo = 0;

  @HostBinding('class.stock-low')
  get stockBajo(): boolean {
    return this.appStockBajo <= 5;
  }
}
