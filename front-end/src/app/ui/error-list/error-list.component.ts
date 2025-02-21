import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-error-list',
  imports: [CommonModule],
  template: `
    <div class="row" *ngIf="erros.length">
    <div class="col">
      <span *ngFor="let erro of erros" class="text-danger py-1">
        {{erro}}
      </span>
    </div>
  </div>
  `,
})
export class ErrorListComponent {
@Input() erros: string[] = []
}
