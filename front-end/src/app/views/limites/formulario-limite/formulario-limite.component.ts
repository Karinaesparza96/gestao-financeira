import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ErrorListComponent } from "../../../ui/error-list/error-list.component";
import { BaseFormComponent } from '../../../base-components/BaseFormComponent';
import { Categoria } from '../../../models/categoria';
import { LimiteOrcamento } from '../../../models/limiteOrcamento';
import { CategoriaService } from '../../../services/categoria.service';
import { LimiteService } from '../../../services/limite.service';
import { NotificacaoService } from '../../../utils/notificacao.service';
import { IValidationMessage } from '../../../utils/validation/IValidationMessage';

@Component({
  selector: 'app-formulario-limite',
  imports: [CommonModule, FormsModule, ReactiveFormsModule, ErrorListComponent],
  templateUrl: './formulario-limite.component.html',
  styleUrl: './formulario-limite.component.scss'
})
export class FormularioLimiteComponent extends BaseFormComponent {

  limites: LimiteOrcamento[] = []
  limiteOrcamentoForm!: FormGroup
  categorias: Categoria[] = []

  mensagens: IValidationMessage = { 
    descricao: {
      required: "O campo descrição é obrigatório."
    },
    limite: {
      required: "O campo limite é obrigatório.",
      min: "O campo valor precisa ser maior que zero."
    },
    porcentagemAviso: {
      required: "O campo porcentagem do aviso é obrigatório.",
    },
    periodo: {
      required: "O campo periodo é obrigatório."
    },
    tipoLimite: {
      required: "O campo tipo limite é obrigatório."
    },
  }

  constructor(
    private fb: FormBuilder,
    private categoriaService: CategoriaService,
    private limiteService: LimiteService,
    private notificacao: NotificacaoService
  ) {
    super();
    this.categoriaService.obterTodos().subscribe(x => this.categorias = x);
    this.limiteOrcamentoForm = this.criarForm();
    this.configurarMensagensValidacaoBase(this.mensagens)
  }


  ngOnInit(): void {
    this.limiteService.obterTodos().subscribe((r) => this.limites = r)
  }

  submit() { }

  private criarForm(): FormGroup {
    return this.fb.group({

    })
  }
}
