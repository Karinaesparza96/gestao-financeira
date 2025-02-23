import { Component, inject, Input } from '@angular/core';
import { NotificacaoService } from '../../utils/notificacao.service';

@Component({
  selector: 'app-clip-board',
  imports: [],
  template: `
  <div class="d-flex align-items-center">
    <span class="transacao-id" [title]="content">
    {{ content }}</span>
    <button 
      class="btn btn-sm"
      (click)="copiarTexto(content)">
      <i class="bi bi-clipboard" [title]="'copiar'"></i>
    </button>
  </div>
  `,
  styles: `
  .transacao-id {
    max-width: 80px;
    white-space: nowrap;
    overflow: hidden;
    text-overflow: ellipsis;
    display: inline-block;
    vertical-align: middle;
    cursor: pointer;
}`
})
export class ClipBoardComponent {
  @Input() content: string = '';
  notificacao = inject(NotificacaoService)

  copiarTexto(texto: string) {
    navigator.clipboard.writeText(texto)
    this.notificacao.mostrarMensagem('Texto copiado para a área de transferência!', 'alerta')
  }
}
