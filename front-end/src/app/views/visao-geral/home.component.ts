import { TipoTransacao } from './../../models/TipoTransacao';
import { Component, viewChild } from '@angular/core';
import { ModalComponent } from "../../ui/modal/modal.component";
import { SaudacaoUsuarioComponent } from "../../ui/saudacao-usuario/saudacao-usuario.component";
import { FormularioTransacaoComponent } from "../transacoes/formulario-transacao/formulario-transacao.component";
import { CommonModule, CurrencyPipe } from '@angular/common';
import { TransacaoService } from '../../services/transacao.service';
import { Transacao } from '../../models/Transacao';
import { ResumoFinanceiro } from '../../models/resumoFinanceiro';
import { Observable } from 'rxjs';
import { GraficoTransacaoComponent } from '../transacoes/grafico-transacao/grafico-transacao.component';

@Component({
  selector: 'app-home',
  imports: [ModalComponent, SaudacaoUsuarioComponent, FormularioTransacaoComponent, CommonModule, CurrencyPipe, GraficoTransacaoComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
  showModal: boolean = false
  modal = viewChild(ModalComponent)
  options: {label: string, cssClass: 'sucesso'|'falha'} = {label: '', cssClass: 'sucesso'};
  tipo: TipoTransacao = TipoTransacao.Entrada
  transacoes: Transacao[] = [
    {
        "id": "c10d72e9-31ac-4b0c-be98-8ecfa5fb3321",
        "categoria": {
            "id": "e8285697-4a2e-4a48-b415-5c3ad156697d",
            "nome": "Alimentação",
            "default": true
        },
        "tipo": 2,
        "data": "2024-12-28T15:30:00",
        "descricao": "teste",
        "valor": 200
    },
    {
        "id": "1182d862-3b9e-4880-a092-ec335246c94a",
        "categoria": {
            "id": "5f8f9205-1aa7-4caa-8cbd-24c2f84a6dc2",
            "nome": "Saúde",
            "default": true
        },
        "tipo": 1,
        "data": "2024-12-28T15:30:00",
        "descricao": "teste",
        "valor": 200
    },
    {
        "id": "8e46bfc8-8577-489f-9d97-342b3d333a27",
        "categoria": {
            "id": "5f8f9205-1aa7-4caa-8cbd-24c2f84a6dc2",
            "nome": "Saúde",
            "default": true
        },
        "tipo": 1,
        "data": "2024-12-28T15:30:00",
        "descricao": "teste",
        "valor": 200
    }
]
 
  resumoSaldo$: Observable<ResumoFinanceiro>;
  
  constructor(private transacaoService: TransacaoService) {
    this.resumoSaldo$ = this.transacaoService.obterResumoTransacoes();
  }

  novaReceita(){
    this.options = {
      label: 'Nova Receita',
      cssClass: 'sucesso'
    };
    this.showModal = true
    this.tipo = TipoTransacao.Entrada
  }
  
  novaDespesa() {
    this.options = {
      label: 'Nova Despesa',
      cssClass: 'falha'
    };
    this.showModal = true
    this.tipo = TipoTransacao.Saida
  }
}
