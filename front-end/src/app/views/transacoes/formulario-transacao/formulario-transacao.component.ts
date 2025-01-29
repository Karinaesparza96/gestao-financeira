import { Component, Input } from '@angular/core';
import { TipoTransacao } from '../../../models/TipoTransacao';
import { FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-formulario-transacao',
  imports: [FormsModule, ReactiveFormsModule],
  templateUrl: './formulario-transacao.component.html',
  styleUrl: './formulario-transacao.component.scss'
})
export class FormularioTransacaoComponent {
@Input() tipoTransacao!: TipoTransacao

formTransacao!: FormGroup
}
