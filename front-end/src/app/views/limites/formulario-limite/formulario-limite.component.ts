import { CommonModule } from '@angular/common';
import { AfterViewInit, Component, ElementRef, EventEmitter, Input, OnInit, Output, QueryList, ViewChildren } from '@angular/core';
import { FormBuilder, FormControlName, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ErrorListComponent } from "../../../components/error-list/error-list.component";
import { BaseFormComponent } from '../../../base-components/BaseFormComponent';
import { Categoria } from '../../../models/categoria';
import { LimiteOrcamento, TipoLimite } from '../../../models/limiteOrcamento';
import { CategoriaService } from '../../../services/categoria.service';
import { LimiteService } from '../../../services/limite.service';
import { NotificacaoService } from '../../../utils/notificacao.service';
import { IValidationMessage } from '../../../utils/validation/IValidationMessage';
import { CurrencyUtils } from '../../../utils/currency-utils';

@Component({
  selector: 'app-formulario-limite',
  imports: [CommonModule, FormsModule, ReactiveFormsModule, ErrorListComponent],
  templateUrl: './formulario-limite.component.html',
  styleUrl: './formulario-limite.component.scss'
})
export class FormularioLimiteComponent extends BaseFormComponent implements OnInit, AfterViewInit {
  @ViewChildren(FormControlName, { read: ElementRef }) formControls!: QueryList<ElementRef>
  @Input() id?: string
  @Output() processouComSucesso = new EventEmitter<void>();
  limiteOrcamentoForm!: FormGroup
  categorias: Categoria[] = []

  mensagens: IValidationMessage = { 
    categoriaId: {
      required: "O campo categoria é obrigatório.",
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
    this.limiteOrcamentoForm = this.criarForm();
    this.categoriaService.obterTodos().subscribe(x => this.categorias = x);
    this.configurarMensagensValidacao(this.mensagens)
  }


  ngOnInit(): void {
   this.carregarLimiteParaEdicao()
   }

  get tipoLimiteGeral() {
    return TipoLimite.geral.toString()
  }

  get tipoLimite() {
    return this.limiteOrcamentoForm.get('tipoLimite')
  }

  ngAfterViewInit(): void {
    this.formControls.changes.subscribe(() => {
      this.validateForm(this.limiteOrcamentoForm, this.formControls);
    });

    this.validateForm(this.limiteOrcamentoForm, this.formControls);
  
    this.limiteOrcamentoForm.get('tipoLimite')?.valueChanges.subscribe((tipo) => {
      tipo == TipoLimite.categoria ? this.adicionarValidacaoCategoria() : this.removerValidacaoCategoria();
    });
  }
  
  submit() { 
    if (this.limiteOrcamentoForm.invalid) return;
    const { id, ...limiteOrcamento } = this.obterLimiteDeForm();
    id ? this.atualizarLimite(id, {...limiteOrcamento, id}) : this.adicionarLimite(limiteOrcamento);
  }

  adicionarValidacaoCategoria() {
    this.limiteOrcamentoForm.get('categoriaId')?.setValidators([Validators.required])
    this.limiteOrcamentoForm.get('categoriaId')?.updateValueAndValidity()
  }

  removerValidacaoCategoria() {
    this.limiteOrcamentoForm.get('categoriaId')?.clearValidators()
    this.limiteOrcamentoForm.get('categoriaId')?.updateValueAndValidity()
    this.limiteOrcamentoForm.get('categoriaId')?.setValue(null)
    this.erros['categoriaId'] = ''
  }

  obterLimiteDeForm() {
    return {
      ...this.limiteOrcamentoForm.value,
      limite: CurrencyUtils.StringParaDecimal(this.limiteOrcamentoForm.value.limite),
      porcentagemAviso: parseFloat(this.limiteOrcamentoForm.value.porcentagemAviso),
      periodo: `${this.limiteOrcamentoForm.value.periodo}-01`
    }
  }

  adicionarLimite(limite: LimiteOrcamento) {
    this.limiteService.adicionar(limite).subscribe({
      next: (r) => this.processarSucesso(r),
      error: (error) => this.processarFalha(error)
    });
  }

  atualizarLimite(id: string, limite: LimiteOrcamento) {
    this.limiteService.atualizar(id, limite).subscribe({
      next: (r) => this.processarSucesso(r),
      error: (error) => this.processarFalha(error)
    });
  }

  criarForm() {
    return this.fb.group({
      id: [null],
      categoriaId: [null],
      periodo: [null, [Validators.required]],
      limite: ['0,00', [Validators.required, Validators.min(0.01)]],
      tipoLimite: [null, [Validators.required]],
      porcentagemAviso: [null, [Validators.required]],
    });
  }

  carregarLimiteParaEdicao() {
    if(this.id) {
      this.limiteService.obterPorId(this.id).subscribe((r) => {
        this.preencherFormLimite(r)
      })
    }
  }

  preencherFormLimite(response: LimiteOrcamento) {
    this.limiteOrcamentoForm.patchValue({
      ...response,
      limite: CurrencyUtils.DecimalParaString(response.limite),
      periodo: response.periodo.toString().substring(0, 7)
    });
  }

  formatarValor({target: {value}}: any) {
    const valor = parseFloat(value.replace(/\D/g, '')) / 100;
    this.limiteOrcamentoForm.get('limite')?.setValue(valor.toFixed(2).replace('.',','));
  }

  private processarSucesso(mensagens: string[]): void {
    this.errosServer = [];
    if (mensagens.length) {
      const avisos = mensagens.join('<br />');
      this.notificacao.show(avisos, 'alerta');
    } else {
      this.notificacao.show("Operação realizada com sucesso!")
    }
    this.processouComSucesso.emit();
  }

  private processarFalha(fail: any): void {
    this.errosServer = fail.error.mensagens;
  }

}
