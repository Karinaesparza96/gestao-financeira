import { TipoTransacao } from './../../models/TipoTransacao';
import { Component, OnInit, viewChild } from '@angular/core';
import { ModalComponent } from "../../ui/modal/modal.component";
import { SaudacaoUsuarioComponent } from "../../ui/saudacao-usuario/saudacao-usuario.component";
import { FormularioTransacaoComponent } from "../transacoes/formulario-transacao/formulario-transacao.component";
import { CommonModule, CurrencyPipe } from '@angular/common';
import { TransacaoService } from '../../services/transacao.service';
import { Transacao } from '../../models/Transacao';
import { ResumoFinanceiro } from '../../models/resumoFinanceiro';
import { Observable } from 'rxjs';
import { GraficoTransacaoComponent } from '../transacoes/grafico-transacao/grafico-transacao.component';

import { NotificacaoService } from '../../utils/notificacao.service';
import { BrCurrencyPipe } from "../../utils/pipes/br-currency.pipe";

@Component({
  selector: 'app-home',
  imports: [ModalComponent, SaudacaoUsuarioComponent, FormularioTransacaoComponent, CommonModule, GraficoTransacaoComponent, BrCurrencyPipe],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent implements OnInit{
  showModal: boolean = false
  modal = viewChild(ModalComponent)
  options: {label: string, cssClass: 'sucesso'|'falha'} = {label: '', cssClass: 'sucesso'};
  tipo: TipoTransacao = TipoTransacao.Entrada
  transacoes: Transacao[] = []
 
  resumoSaldo$: Observable<ResumoFinanceiro>;

  constructor(private transacaoService: TransacaoService,
              private notificacao: NotificacaoService
  ) {
    this.resumoSaldo$ = this.transacaoService.obterResumoTransacoes();
  }

  ngOnInit(): void {
    this.transacaoService.obterTodos().subscribe((x) => this.transacoes = x)
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

  processarSucesso() {
    this.showModal = false
    this.notificacao.mostrarMensagem('Operação realizada com sucesso!','sucesso')
  }
}
