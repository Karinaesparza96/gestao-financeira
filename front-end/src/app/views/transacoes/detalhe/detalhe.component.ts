import { Component, Input } from '@angular/core';
import { Transacao } from '../../../models/Transacao';
import { BrCurrencyPipe } from "../../../utils/pipes/br-currency.pipe";
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-detalhe',
  imports: [BrCurrencyPipe, CommonModule],
  templateUrl: './detalhe.component.html',
  styleUrl: './detalhe.component.scss'
})
export class DetalheComponent {
@Input() transacao?: Transacao;
}
