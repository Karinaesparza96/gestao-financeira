import { CommonModule } from '@angular/common';
import { Component, Input, Output, EventEmitter } from '@angular/core';
import { BrCurrencyPipe } from '../../utils/pipes/br-currency.pipe';
import { ClipBoardComponent } from "../clip-board/clip-board.component";

@Component({
  selector: 'app-tabela',
  imports: [CommonModule, BrCurrencyPipe, ClipBoardComponent],
  templateUrl: './tabela.component.html',
})
export class TabelaComponent {
  @Input() colunas: { campo: string, titulo: string, classe?: string, pipe?: string, classeDinamica?: string }[] = [];
  @Input() dados: any[] = [];
  @Input() acoes: { icone: string, classe: string, acao: (item: any) => void }[] = [];
  @Output() acaoExecutada = new EventEmitter<any>();
  @Input() componenteParent: any;

  executarAcao(acao: any, item: any) {
    if (acao.acao) {
      acao.acao(item);
    } else {
      this.acaoExecutada.emit({ acao, item });
    }
  }

  // Método para aplicar classe dinâmica
  aplicarClasseDinamica(coluna: any, item: any): string {
    if (coluna.classeDinamica && this.componenteParent &&
        typeof this.componenteParent[coluna.classeDinamica] === 'function') {
      return this.componenteParent[coluna.classeDinamica](item);
    }
    return '';
  }
}
