import { CommonModule } from '@angular/common';
import { AfterViewInit, Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChildren } from '@angular/core';
import { FormBuilder, FormControlName, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { fromEvent, merge, Observable } from 'rxjs';
import { FormValidationService } from '../../../utils/validation/form-validation.service';
import { TipoTransacao } from '../../../models/TipoTransacao';
import { Categoria } from '../../../models/categoria';
import { CategoriaService } from '../../../services/categoria.service';
import { IDisplayMessage } from '../../../utils/validation/IValidationMessage';
import { TransacaoService } from '../../../services/transacao.service';
import { Transacao } from '../../../models/Transacao';
import { NotificacaoService } from '../../../utils/notificacao.service';

@Component({
  selector: 'app-formulario-transacao',
  imports: [FormsModule, ReactiveFormsModule, CommonModule],
  templateUrl: './formulario-transacao.component.html',
  styleUrl: './formulario-transacao.component.scss'
})
export class FormularioTransacaoComponent implements OnInit, AfterViewInit {
  @Input() tipo!: TipoTransacao;
  @ViewChildren(FormControlName, { read: ElementRef }) formControls?: ElementRef[]
  categorias$: Observable<Categoria[]>;
  formTransacao!: FormGroup
  erros: IDisplayMessage = {}
  transacao?: Transacao
  errosServer: string[] = []
  @Input()
  set id(value: any) {
    if (typeof value == 'string') {
      this.transacaoService.obterPorId(value).subscribe((data) => this.preencherFormTransacao(data))
    }
  }
  @Output() processouComSucesso = new EventEmitter<boolean>()
  constructor(private fb: FormBuilder,
    private categoriaService: CategoriaService,
    private transacaoService: TransacaoService,
    private formValidation: FormValidationService,
    private notificacao: NotificacaoService) {
    this.initForm()
    this.categorias$ = this.categoriaService.obterTodos();
  }

  initForm() {
    this.formTransacao = this.fb.group({
      id: [null],
      descricao: ['', [Validators.required]],
      valor: ['', [Validators.required]],
      data: ['', [Validators.required]],
      tipo: ['', [Validators.required]],
      categoria: this.fb.group({
        id: [null, [Validators.required]],
        nome: '',
        default: false
      }),
    })
  }

  ngOnInit(): void {
    this.adicionaTipoTransacao();
  }

  ngAfterViewInit(): void {
    const controlsBlurs$: Observable<any>[] = this.formControls?.map((fc: ElementRef) => fromEvent(fc.nativeElement, 'blur')) ?? []

    if (controlsBlurs$.length)
      merge(...controlsBlurs$).subscribe(() => {
        this.erros = this.validar(this.formTransacao)
      })
  }

  adicionaTipoTransacao() {
    if (this.tipo) {
      this.formTransacao.patchValue({ tipo: this.tipo })
    }
  }

  validar(formGroup: FormGroup) {
    return this.formValidation.executeValidation(formGroup, 'transacao')
  }

  onCategoriaChange(id: string) {
    this.categorias$.subscribe(categorias => {
      const categoriaSelecionada = categorias.find(c => c.id === id);
      if (categoriaSelecionada) {
        this.formTransacao.patchValue({
          categoria: {
            id: categoriaSelecionada.id,
            nome: categoriaSelecionada.nome
          }
        });
      }
    });
  }

  formatarValor() {
    let valor = this.formTransacao.get('valor')?.value + '';
    if (valor) {
      valor = valor.replace(/\./g, '').replace(',', '.');
      const valorDecimal = parseFloat(valor);

      if (!isNaN(valorDecimal)) {
        this.formTransacao.patchValue({ valor: valorDecimal });
        this.erros['valor'] = '';
        this.formTransacao.get('valor')?.setErrors(null);
      } else {
        this.erros['valor'] = 'O valor inserido não é válido';
        this.formTransacao.get('valor')?.setErrors({ invalidValue: true });
      }
    }
  }

  submit() {
    this.formatarValor()
    if (!this.formTransacao.valid) {
      return
    }
    const transacao = {
      ...this.formTransacao.value,
      data: new Date(this.formTransacao.value.data)
    }
    if (transacao.id) {
      this.transacaoService.atualizar(transacao.id, transacao)
        .subscribe({
          next: (r) => this.processarSucesso(r as string[]),
          error: (e) => this.processarFalha(e)
        })
    } else {
      this.transacaoService.adicionar(transacao)
        .subscribe({
          next: (r) => this.processarSucesso(r),
          error: (e) => this.processarFalha(e)
        })
    }
  }

  preencherFormTransacao(response: Transacao) {
    this.formTransacao.patchValue({
      id: response.id,
      descricao: response.descricao,
      valor: response.valor,
      data: response.data.split('T')[0],
      tipo: response.tipo,
      categoria: {
        id: response.categoria?.id,
        nome: response.categoria?.nome,
        default: response.categoria?.default
      }
    })
  }

  processarSucesso(response: string[]) {
    this.errosServer = []
    if (response.length) {
      let avisos = response.join('<br />')
      this.notificacao.mostrarMensagem(avisos, 'alerta')
    }
    this.processouComSucesso.emit(true)
  }
  processarFalha(fail: any) {
    this.errosServer = fail.error.mensagens;
  }
}