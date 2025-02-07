import { CommonModule } from '@angular/common';
import { AfterViewInit, Component, ElementRef, Input, OnInit, ViewChildren } from '@angular/core';
import { FormBuilder, FormControlName, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { fromEvent, merge, Observable } from 'rxjs';
import { FormValidationService } from '../../../utils/validation/form-validation.service';
import { TipoTransacao } from '../../../models/TipoTransacao';
import { Categoria } from '../../../models/categoria';
import { CategoriaService } from '../../../services/categoria.service';
import { IDisplayMessage } from '../../../utils/validation/IValidationMessage';
import { TransacaoService } from '../../../services/transacao.service';

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
      descricao: ['', [Validators.required]],
      valor: ['', [Validators.required]],
      data: ['', [Validators.required]],
      categoria: ['', [Validators.required]],
      tipo: ['']
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
    if (this.tipo == TipoTransacao.Entrada) {
      this.formTransacao.get('tipo')?.setValue(TipoTransacao.Entrada)
      return
    } 
    if (this.tipo == TipoTransacao.Saida) {
      this.formTransacao.get('tipo')?.setValue(TipoTransacao.Saida)
    }
  }

  validar(formGroup: FormGroup) {
   return this.formValidation.executeValidation(formGroup, 'transacao')
  }

  submit() {
   const form = {...this.formTransacao.value, 
      data: new Date(this.formTransacao.value.data), 
      valor:  this.formTransacao.value.valor.replace(',', '.')}
   console.log(form)
   this.transacaoService.adicionar(form).subscribe()
  }
}
