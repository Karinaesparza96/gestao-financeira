import { Component, Input } from '@angular/core';
import { ResumoFinanceiro } from '../../../models/resumoFinanceiro';
import { CommonModule } from '@angular/common';
import { BrCurrencyPipe } from "../../../utils/pipes/br-currency.pipe";

@Component({
  selector: 'app-resumo-financeiro',
  imports: [CommonModule, BrCurrencyPipe],
  templateUrl: './resumo-financeiro.component.html',
  styleUrl: './resumo-financeiro.component.scss'
})
export class ResumoFinanceiroComponent {
@Input() dados?: ResumoFinanceiro;
}
