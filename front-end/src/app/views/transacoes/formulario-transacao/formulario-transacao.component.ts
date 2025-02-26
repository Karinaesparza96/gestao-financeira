import { CommonModule } from '@angular/common';
import { AfterViewInit, Component, ElementRef, EventEmitter, Input, OnInit, Output, QueryList, ViewChildren } from '@angular/core';
import { FormBuilder, FormControlName, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { TipoTransacao } from '../../../models/TipoTransacao';
import { Categoria } from '../../../models/categoria';
import { CategoriaService } from '../../../services/categoria.service';
import { TransacaoService } from '../../../services/transacao.service';
import { Transacao } from '../../../models/Transacao';
import { NotificacaoService } from '../../../utils/notificacao.service';
import { ErrorListComponent } from "../../../ui/error-list/error-list.component";
import { BaseFormComponent } from '../../../base-components/BaseFormComponent';
import { IValidationMessage } from '../../../utils/validation/IValidationMessage';

@Component({
  selector: 'app-formulario-transacao',
  imports: [FormsModule, ReactiveFormsModule, CommonModule, ErrorListComponent],
  templateUrl: './formulario-transacao.component.html',
  styleUrl: './formulario-transacao.component.scss'
})
export class FormularioTransacaoComponent extends BaseFormComponent implements OnInit, AfterViewInit {
  @ViewChildren(FormControlName, { read: ElementRef }) formControls?: QueryList<ElementRef>
  formGroup!: FormGroup

  @Input() tipo!: TipoTransacao;
  @Input() id?: string
  @Output() processouComSucesso = new EventEmitter<boolean>();

  mensagens: IValidationMessage = {
    descricao: {
      required: "O campo descrição é obrigatório."
    },
    valor: {
      required: "O campo valor é obrigatório.",
      min: "O campo valor precisa ser maior que zero."
    },
    data: {
      required: "O campo data é obrigatório."
    },
    tipo: {
      required: "O campo tipo é obrigatório."
    },
    idCategoria: {
      required: "O campo categoria é obrigatório."
    }
  }
  
  categorias: Categoria[] = [];
  transacao?: Transacao;

  constructor(
    private fb: FormBuilder,
    private categoriaService: CategoriaService,
    private transacaoService: TransacaoService,
    private notificacao: NotificacaoService
  ) {
    super();
    this.categoriaService.obterTodos().subscribe(x => this.categorias = x);
    this.formGroup = this.criarForm();
    this.configurarMensagensValidacao(this.mensagens)
  }

  ngOnInit(): void {
    this.adicionaTipoTransacao();
    this.carregarTransacaoParaEdicao();
  }

  ngAfterViewInit(): void {
    if (this.formControls) {
      this.validateForm(this.formGroup, this.formControls);
    }
  }

  submit(): void {
    if (!this.formGroup.valid) return;
    const {id, ...transacao} = this.criarTransacaoDeForm();
    id ? this.atualizarTransacao(id, {...transacao, id}) : this.adicionarTransacao(transacao);
}

  private criarForm(): FormGroup {
    return this.fb.group({
      id: [null],
      descricao: ['', [Validators.required]],
      valor: ['', [Validators.required, Validators.min(0.01)]],
      data: ['', [Validators.required]],
      tipo: ['', [Validators.required]],
      categoria: this.fb.group({
        idCategoria: [null, [Validators.required]],
        nome: '',
        default: false
      })
    });
  }

  private adicionaTipoTransacao(): void {
    if (this.tipo) {
      this.formGroup.patchValue({ tipo: this.tipo });
    }
  }

  private carregarTransacaoParaEdicao() {
    if (this.id) {
      this.transacaoService.obterPorId(this.id).subscribe(data => this.preencherFormTransacao(data));
    }
  }

  private criarTransacaoDeForm(): Transacao {
  const categoria = this.categorias.find(x => x.id === this.formGroup.get('categoria.idCategoria')?.value);
      return {
          ...this.formGroup.value,
          data: new Date(this.formGroup.value.data),
          categoria: {
            id: categoria?.id,
            nome: categoria?.nome,
            default: categoria?.default
          }
      };
  }

  private atualizarTransacao(id: string, transacao: Transacao): void {
      this.transacaoService.atualizar(id, transacao).subscribe({
          next: r => this.processarSucesso(r as string[]),
          error: e => this.processarFalha(e)
      });
  }

  private adicionarTransacao(transacao: Transacao): void {
      this.transacaoService.adicionar(transacao).subscribe({
          next: r => this.processarSucesso(r),
          error: e => this.processarFalha(e)
      });
  }

  private preencherFormTransacao(response: Transacao): void {
    this.formGroup.patchValue({
      id: response.id,
      descricao: response.descricao,
      valor: response.valor,
      data: response.data.split('T')[0],
      tipo: response.tipo,
      categoria: {
        idCategoria: response.categoria?.id,
        nome: response.categoria?.nome,
        default: response.categoria?.default
      }
    });
  }

  formatarValor({target: {value}}: any): void {
    const valor = parseFloat(value.replace(/\D/g, '')) / 100;
    this.formGroup.get('valor')?.setValue(valor.toFixed(2));
  }

  private processarSucesso(response: string[]): void {
    this.errosServer = [];
    if (response.length) {
      const avisos = response.join('<br />');
      this.notificacao.show(avisos, 'alerta');
    }
    this.processouComSucesso.emit(true);
  }

  private processarFalha(fail: any): void {
    this.errosServer = fail.error.mensagens;
  }
}