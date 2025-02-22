import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { LimiteOrcamento } from '../../models/limiteOrcamento';
import { LimiteService } from '../../services/limite.service';
import { ModalComponent } from "../../ui/modal/modal.component";
import { EmptyStateComponent } from '../../ui/empty-state/empty-state.component';
import { CategoriaService } from '../../services/categoria.service';
import { NotificacaoService } from '../../utils/notificacao.service';
import { BaseFormComponent } from '../../base-components/BaseFormComponent';
import { Categoria } from '../../models/categoria';
import { FormularioLimiteComponent } from "./formulario-limite/formulario-limite.component";
import { BrCurrencyPipe } from "../../utils/pipes/br-currency.pipe";
import { ClipBoardComponent } from "../../ui/clip-board/clip-board.component";

@Component({
  selector: 'app-limites',
  imports: [CommonModule, ModalComponent, EmptyStateComponent, FormularioLimiteComponent, BrCurrencyPipe, ClipBoardComponent],
  templateUrl: './limites.component.html',
  styleUrl: './limites.component.scss'
})
export class LimitesComponent extends BaseFormComponent implements OnInit {
  limites: LimiteOrcamento[] = []
  categorias: Categoria[] = []
  showModalNovo: boolean = false;
  showModalEditar: boolean = false;
  showModalExcluir: boolean = false;
  limiteId?: string;
  
    constructor(
      private categoriaService: CategoriaService,
      private limiteService: LimiteService,
      private notificacao: NotificacaoService
    ) {
      super();
      this.categoriaService.obterTodos().subscribe(x => this.categorias = x);
    }

  ngOnInit(): void {
    this.limiteService.obterTodos().subscribe((r) => this.limites = r)
  }
  
  editarLimite(limiteId: string) {
    this.limiteId = limiteId;
    this.showModalEditar = true;
  }

  excluirLimite(limiteId: string) {
    this.limiteId = limiteId;
    this.showModalExcluir = true;
  }

  fecharModal() {
    this.showModalNovo = false;
    this.showModalEditar = false;
    this.showModalExcluir = false;
    this.limiteId = undefined;
  }

  atualizar() {
    this.limiteService.obterTodos().subscribe((r) => this.limites = r)
    this.categoriaService.obterTodos().subscribe(x => this.categorias = x);
  }

  processarSucesso() {
    this.atualizar();
    this.fecharModal();
    this.notificacao.mostrarMensagem("Operação realizada com sucesso.")
  }

}
