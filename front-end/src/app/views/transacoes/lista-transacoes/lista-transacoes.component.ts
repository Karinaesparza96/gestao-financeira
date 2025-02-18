import { NotificacaoService } from './../../../utils/notificacao.service';
import { Component, OnInit } from '@angular/core';
import { TransacaoService } from '../../../services/transacao.service';
import { Transacao } from '../../../models/Transacao';
import { ResumoFinanceiro } from '../../../models/resumoFinanceiro';
import { CommonModule } from '@angular/common';
import { ModalComponent } from "../../../ui/modal/modal.component";
import { FormularioTransacaoComponent } from "../formulario-transacao/formulario-transacao.component";
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-lista-transacoes',
  imports: [CommonModule, ModalComponent, FormularioTransacaoComponent, RouterModule],
  templateUrl: './lista-transacoes.component.html',
  styleUrl: './lista-transacoes.component.scss'
})
export class ListaTransacoesComponent implements OnInit {
  transacoes: Transacao[] = []
  resumo?: ResumoFinanceiro
  transacao?: Transacao | null
  showModalEditar: boolean = false
  showModalExcluir: boolean = false
  showModalNovo: boolean = false
  constructor(private transacaoService: TransacaoService, private notificacao: NotificacaoService) { }

  ngOnInit(): void {
    this.atualizar()
  }

  editarTransacao(transacao: Transacao) {
    this.transacao = transacao
    this.showModalEditar = true
  }

  excluirTransacao(transacao: Transacao) {
    this.transacao = transacao
    this.showModalExcluir = true
  }

  fecharModal() {
    this.transacao = null
    this.showModalEditar = false
    this.showModalExcluir = false
    this.showModalNovo = false
  }

  atualizar() {
    this.transacaoService.obterResumoTransacoes().subscribe(x => this.resumo = x)
    this.transacaoService.obterTodos().subscribe(x => this.transacoes = x)
  }

  excluir() {
    if (this.transacao?.id)
      this.transacaoService.excluir(this.transacao?.id)
        .subscribe({
          next: () => this.processarSucesso(),
          error: () => this.processarErro()
        })
  }

  processarSucesso() {
    this.atualizar()
    this.fecharModal()
    this.notificacao.mostrarMensagem('Operação realizada com sucesso!', 'sucesso')
  }

  processarErro() {
    this.notificacao.mostrarMensagem('Ops! Houve um erro', 'falha')
  }
}
