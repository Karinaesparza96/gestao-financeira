import { Component, Input } from '@angular/core';
import { LimiteOrcamento } from '../../../models/limiteOrcamento';
import { CommonModule } from '@angular/common';
import { BrCurrencyPipe } from "../../../utils/pipes/br-currency.pipe";

@Component({
  selector: 'app-detalhe-limite',
  imports: [CommonModule, BrCurrencyPipe],
  templateUrl: './detalhe-limite.component.html',
  styleUrl: './detalhe-limite.component.scss'
})
export class DetalheLimiteComponent {
@Input() limite?: LimiteOrcamento;
}
