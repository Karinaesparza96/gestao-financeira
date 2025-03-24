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
  @Input() colunas: { campo: string, titulo: string, classe?: string, pipe?: string, classeDinamica?: (item: any) => any , tratativa?: (item: any) => any  }[] = [];
  @Input() dados: any[] = [];
  @Input() acoes: { icone: string, classe: string, acao: (item: any) => void }[] = [];

}
