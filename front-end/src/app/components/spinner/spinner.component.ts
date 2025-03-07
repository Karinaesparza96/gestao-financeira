import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, Input } from '@angular/core';

@Component({
  selector: 'app-spinner',
  imports: [CommonModule],
  template: `<div *ngIf="carregando" class="text-center py-5">
  <div class="spinner-border text-primary" role="status">
    <span class="visually-hidden">Carregando...</span>
  </div>
  <p class="mt-2">Carregando...</p>
</div>`,
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class SpinnerComponent { 
  @Input() carregando: boolean = false;
}
