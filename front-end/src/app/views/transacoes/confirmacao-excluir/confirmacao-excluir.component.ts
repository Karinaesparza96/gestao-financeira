import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Transacao } from '../../../models/Transacao';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-confirmacao-excluir',
  imports: [CommonModule],
  templateUrl: './confirmacao-excluir.component.html',
  styleUrl: './confirmacao-excluir.component.scss'
})
export class ConfirmacaoExcluirComponent {
  @Input() transacao?: Transacao | null
  @Output() changed = new EventEmitter<boolean>()

  confirmar() {
    this.changed.emit(true)
  }
  cancelar() {
    this.changed.emit(false)
  }
}
