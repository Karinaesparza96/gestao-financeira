import { CommonModule } from '@angular/common';
import { Component, input, Input } from '@angular/core';

@Component({
  selector: 'app-empty-state',
  imports: [CommonModule],
  template: `
    <div class="text-center py-5" *ngIf="condicao">
    <i class="bi bi-inbox fs-1 text-muted"></i>
    <p class="mt-2 text-muted">
     {{ conteudo }}
    </p>
  </div>
  `,
})
export class EmptyStateComponent {
@Input() condicao: boolean = false
@Input() conteudo: string = 'Sem registros'
}
