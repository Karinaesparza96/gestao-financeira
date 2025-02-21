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

@Component({
  selector: 'app-limites',
  imports: [CommonModule, ModalComponent, EmptyStateComponent],
  templateUrl: './limites.component.html',
  styleUrl: './limites.component.scss'
})
export class LimitesComponent extends BaseFormComponent implements OnInit {
  limites: LimiteOrcamento[] = []
  categorias: Categoria[] = []
  
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

}
