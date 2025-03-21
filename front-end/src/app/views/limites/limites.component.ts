import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { LimiteOrcamento } from '../../models/limiteOrcamento';
import { LimiteService } from '../../services/limite.service';
import { ModalComponent } from "../../components/modal/modal.component";
import { EmptyStateComponent } from '../../components/empty-state/empty-state.component';
import { CategoriaService } from '../../services/categoria.service';
import { NotificacaoService } from '../../utils/notificacao.service';
import { BaseFormComponent } from '../../base-components/BaseFormComponent';
import { Categoria } from '../../models/categoria';
import { FormularioLimiteComponent } from "./formulario-limite/formulario-limite.component";
import { TabelaComponent } from "../../components/tabela/tabela.component";
import { map } from 'rxjs';
import { ConfirmacaoExcluirComponent } from "../../components/confirmacao-excluir/confirmacao-excluir.component";
import { DetalheLimiteComponent } from "./detalhe-limite/detalhe-limite.component";
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { SpinnerComponent } from "../../components/spinner/spinner.component";
import { SpinnerService } from '../../components/spinner/spinner.service';

@Component({
  selector: 'app-limites',
  imports: [CommonModule, ModalComponent, EmptyStateComponent, FormularioLimiteComponent, TabelaComponent, ConfirmacaoExcluirComponent, DetalheLimiteComponent, ReactiveFormsModule, SpinnerComponent],
  templateUrl: './limites.component.html',
  styleUrl: './limites.component.scss'
})
export class LimitesComponent extends BaseFormComponent implements OnInit {
  limites: any[] = []
  categorias: Categoria[] = []
  showModalNovo: boolean = false;
  showModalEditar: boolean = false;
  showModalExcluir: boolean = false;
  limite?: LimiteOrcamento;
  filtroForm!: FormGroup;
  tabela = {
    colunas: [
      {campo: 'id', titulo: '#', classe: '', pipe: 'id'},
      {campo: 'periodo', titulo: 'Periodo', classe: '', pipe: 'date'},
      {campo: 'tipoLimite', titulo: 'Tipo', classe: ''},
      {campo: 'categoria', titulo: 'Categoria', classe: ''},
      {campo: 'limite', titulo: 'Limite', classe: 'text-end', pipe: 'currency'},
      {campo: 'porcentagemAviso', titulo: '% Aviso', classe: 'text-end', pipe: 'percent'},
      {campo: 'limiteUtilizado', titulo: 'Utilizado', classe: 'text-end', pipe: 'currency', classeDinamica: 'getPercentualClasse'},
      {campo: 'percentualLimiteUtilizado', titulo: '% Utilizado', classe: 'text-end', classeDinamica: 'getPercentualClasse'}
    ],
    acoes: [
      {icone: 'bi-pencil', classe: 'btn-outline-primary me-1', acao: this.editarLimite.bind(this)},
      {icone: 'bi-trash', classe: 'btn-outline-danger', acao: this.excluirLimite.bind(this)}
    ]
  }
  carregando: boolean = false;

    constructor(
      private fb: FormBuilder,
      private categoriaService: CategoriaService,
      private limiteService: LimiteService,
      private notificacao: NotificacaoService,
      private spinnerService: SpinnerService
    ) {
      super();
      this.spinnerService.loading$.subscribe(x => this.carregando = x);
      this.filtroForm = this.fb.group({
        periodo: [''],
        categoriaId: ['']
      });
    }

  ngOnInit(): void {
   this.atualizar();
  }

  editarLimite(limite: LimiteOrcamento) {
    this.limite = limite;
    this.showModalEditar = true;
  }

  excluirLimite(limite: LimiteOrcamento) {
    this.limite = limite;
    this.showModalExcluir = true;
  }

  fecharModal() {
    this.showModalNovo = false;
    this.showModalEditar = false;
    this.showModalExcluir = false;
    this.limite = undefined;
  }

  atualizar() {
    this.filtrar();
    this.categoriaService.obterTodos().subscribe(x => this.categorias = x);
  }

  processarSucesso() {
    this.atualizar();
    this.fecharModal();
  }

  processarErro() {
    this.notificacao.show('Ops! Houve um erro', 'falha')
  }

  confirmarExcluir() {
    if (this.limite?.id) {
      this.limiteService.excluir(this.limite?.id)
        .subscribe({
          next: () => {
            this.processarSucesso()
            this.notificacao.show('Limite excluído com sucesso')
          },
          error: () => this.processarErro()
        })
    }
  }

  corresponderAcaoExcluir(confirmou: boolean) {
    confirmou ? this.confirmarExcluir() : this.fecharModal()
  }

  filtrar() {
    const filtro = this.filtroForm.value;
    this.limiteService.obterTodosComFiltro(filtro).pipe(map((x: LimiteOrcamento[]) => x.map((y: LimiteOrcamento) => {
      return {
        ...y,
        tipoLimite: y.tipoLimite == 1 ? 'Geral' : 'Categoria',
        categoria: y.categoriaNome || 'Geral'
      }
    }))).subscribe((r) => this.limites = r)
  }

  getPercentualClasse(item: any): string {
    const percentual = parseFloat(item.percentualLimiteUtilizado?.replace('%', '') || '0');
    const porcentagemAviso = item.porcentagemAviso;
    const percentualMaximo = 100;

    if (percentual >= percentualMaximo) {
      return 'text-danger fw-bold';
    }

    if (percentual >= porcentagemAviso) {
      return 'text-warning fw-bold';
    }

    return 'text-success fw-bold';
  }
}
