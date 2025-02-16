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
import { FormDataUtils } from '../../../utils/formDataUtils';
import { NotificationService } from '../../../services/notification.service';
import { Router } from '@angular/router';

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
  if(typeof value == 'string'){
    this.transacaoService.obterPorId(value).subscribe((data) => this.preencherFormTransacao(data))
  }
}
@Output() processouComSucesso = new EventEmitter<boolean>()
  constructor(private fb: FormBuilder, 
              private categoriaService: CategoriaService, 
              private transacaoService: TransacaoService,
              private formValidation: FormValidationService) 
  {
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
      categoria: [null, [Validators.required]],
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

  adicionaTipoTransacao(){
    if (this.tipo) {
      this.formTransacao.patchValue({tipo: this.tipo})
    }
  }

  validar(formGroup: FormGroup) {
   return this.formValidation.executeValidation(formGroup, 'transacao')
  }

  submit() {
   const form = {...this.formTransacao.value, 
      data: new Date(this.formTransacao.value.data), 
      valor:  this.formTransacao.value.valor}
      console.log(form)
   this.transacaoService.adicionar(form).subscribe()
  }

  processarSucesso(response: Transacao) {
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
}
