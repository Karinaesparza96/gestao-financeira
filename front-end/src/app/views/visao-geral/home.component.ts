import { TipoTransacao } from './../../models/TipoTransacao';
import { Component, OnInit, viewChild } from '@angular/core';
import { ModalComponent } from "../../components/modal/modal.component";
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { SaudacaoUsuarioComponent } from "../../components/saudacao-usuario/saudacao-usuario.component";
import { FormularioTransacaoComponent } from "../transacoes/formulario-transacao/formulario-transacao.component";
import { TransacaoService } from '../../services/transacao.service';
import { Transacao } from '../../models/Transacao';
import { ResumoFinanceiro } from '../../models/resumoFinanceiro';
import { GraficoTransacaoComponent } from '../transacoes/grafico-transacao/grafico-transacao.component';
import { NotificacaoService } from '../../utils/notificacao.service';
import { BrCurrencyPipe } from "../../utils/pipes/br-currency.pipe";
import { GraficoService } from '../../services/grafico.service';

@Component({
  selector: 'app-home',
  imports: [ModalComponent, SaudacaoUsuarioComponent, FormularioTransacaoComponent, CommonModule, GraficoTransacaoComponent, BrCurrencyPipe],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
  providers: [GraficoService]
})
export class HomeComponent implements OnInit{
  showModal: boolean = false
  modal = viewChild(ModalComponent)
  options: {label: string, cssClass: 'sucesso'|'falha'} = {label: '', cssClass: 'sucesso'};
  tipo: TipoTransacao = TipoTransacao.Entrada
  transacoes: Transacao[] = []
  entradasEDespesas: any
  maioresGastos: any
  resumoSaldo$: Observable<ResumoFinanceiro>;

  constructor(private transacaoService: TransacaoService,
              private notificacao: NotificacaoService,
              private graficoService: GraficoService
  ) {
    this.resumoSaldo$ = this.transacaoService.obterResumoTransacoes();
  }

  ngOnInit(): void {
    this.atualizar()
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

  atualizar() {
    this.transacaoService.obterTodos().subscribe((x) => this.transacoes = x)
    this.graficoService.obterDados().subscribe(({entradasEDespesas, maioresGastos}) => {
      this.entradasEDespesas = entradasEDespesas
      this.maioresGastos = maioresGastos
    })
  }

  processarSucesso() {
    this.atualizar()
    this.showModal = false
    this.notificacao.show('Operação realizada com sucesso!','sucesso')
  }
}
