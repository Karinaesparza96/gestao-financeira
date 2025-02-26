import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-error-list',
  imports: [CommonModule],
  template: `
  <div class="alert alert-danger" *ngIf="erros.length > 0">
    <ul>
      <li *ngFor="let error of erros">{{ error }}</li>
    </ul>
  </div>
  `,
})
export class ErrorListComponent {
@Input() erros: string[] = []
}
