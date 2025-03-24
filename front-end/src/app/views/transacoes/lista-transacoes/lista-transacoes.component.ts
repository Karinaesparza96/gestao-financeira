import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { map, Observable } from 'rxjs';
import { TransacaoService } from '../../../services/transacao.service';
import { NotificacaoService } from './../../../utils/notificacao.service';
import { Transacao } from '../../../models/Transacao';
import { ResumoFinanceiro } from '../../../models/resumoFinanceiro';
import { ModalComponent } from "../../../components/modal/modal.component";
import { FormularioTransacaoComponent } from "../formulario-transacao/formulario-transacao.component";
import { EmptyStateComponent } from "../../../components/empty-state/empty-state.component";
import { ResumoFinanceiroComponent } from "../resumo-financeiro/resumo-financeiro.component";
import { TabelaComponent } from '../../../components/tabela/tabela.component';
import { DetalheComponent } from "../detalhe/detalhe.component";
import { ConfirmacaoExcluirComponent } from "../../../components/confirmacao-excluir/confirmacao-excluir.component";
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CategoriaService } from '../../../services/categoria.service';
import { Categoria } from '../../../models/categoria';
import { SpinnerComponent } from "../../../components/spinner/spinner.component";
import { SpinnerService } from '../../../components/spinner/spinner.service';
import { TipoTransacao } from '../../../models/TipoTransacao';

@Component({
  selector: 'app-lista-transacoes',
  imports: [CommonModule, ModalComponent, FormularioTransacaoComponent, RouterModule, EmptyStateComponent, ResumoFinanceiroComponent, TabelaComponent, DetalheComponent, ConfirmacaoExcluirComponent, ReactiveFormsModule, SpinnerComponent],
  templateUrl: './lista-transacoes.component.html',
})
export class ListaTransacoesComponent implements OnInit {
  transacoes?: any[] = []
  resumo?: ResumoFinanceiro
  transacao?: Transacao | null
  showModalEditar: boolean = false
  showModalExcluir: boolean = false
  showModalNovo: boolean = false
  filtroForm!: FormGroup
  categorias: Categoria[] = []
  tabela = {
    colunas: [
      { campo: 'id', titulo: '#', pipe: 'id' },
      { campo: 'data', titulo: 'Data', pipe: 'date' },
      { campo: 'descricao', titulo: 'Descrição' },
      { campo: 'categoria', titulo: 'Categoria' },
      { campo: 'tipo', titulo: 'Tipo', classeDinamica: this.obterTipoClasse.bind(this), tratativa: this.tratarExibicaoTipo.bind(this) },
      { campo: 'valor', titulo: 'Valor', pipe: 'currency' }
    ],
    acoes: [
      { icone: 'bi-pencil', classe: 'btn-outline-primary me-1', acao: this.editarTransacao.bind(this) },
      { icone: 'bi-trash', classe: 'btn-outline-danger', acao: this.excluirTransacao.bind(this) }
    ]
  }
carregando: boolean = false;
  constructor(private fb: FormBuilder, 
            private categoriaService: CategoriaService, 
            private transacaoService: TransacaoService, 
            private notificacao: NotificacaoService,
            private spinnerService: SpinnerService) {
    this.spinnerService.loading$.subscribe(x => this.carregando = x);
    this.filtroForm = this.fb.group({
      data: [''],
      categoriaId: [''],
      tipoTransacao: ['']
    });
  }

  ngOnInit(): void {
    this.atualizar()
    this.categoriaService.obterTodos().subscribe(x => this.categorias = x)
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
    this.obterTodosComFiltro().subscribe(x => this.transacoes = x)
  }

  excluir() {
    if (this.transacao?.id)
      this.transacaoService.excluir(this.transacao?.id)
        .subscribe({
          next: () => {
            this.processarSucesso(),
            this.notificacao.show('Transação excluída com sucesso')
          },
          error: () => this.processarErro()
        })
  }

  corresponderAcaoExcluir(confirmou: boolean) {
    confirmou ? this.excluir() : this.fecharModal()
  }

  processarSucesso() {
    this.atualizar()
    this.fecharModal()
  }

  processarErro() {
    this.notificacao.show('Ops! Houve um erro', 'falha')
  }

  filtrar() {
    this.obterTodosComFiltro().subscribe(x => this.transacoes = x)
  }

  obterTodosComFiltro() {
    const filtro = this.filtroForm.value
    return this.transacaoService.obterTodosComFiltro(filtro).pipe(
      map((x: Transacao[]) => x.map(y => ({
        ...y,
        categoria: y.categoria?.nome
      })))
    )
  }

  obterTipoClasse(item: any): string {
    return item.tipo == TipoTransacao.Saida ? 'text-danger fw-bold' : 'text-success fw-bold'
  }

  tratarExibicaoTipo(item: any): string {
    return item.tipo == TipoTransacao.Saida ? 'Despesa' : 'Receita'
  }

}
