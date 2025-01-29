import { Component, viewChild, ViewChild } from '@angular/core';
import { ModalComponent } from "../../ui/modal/modal.component";
import { NgTemplateOutlet } from '@angular/common';
import { SaudacaoUsuarioComponent } from "../../ui/saudacao-usuario/saudacao-usuario.component";
import { FormularioTransacaoComponent } from "../transacoes/formulario-transacao/formulario-transacao.component";

@Component({
  selector: 'app-home',
  imports: [ModalComponent, SaudacaoUsuarioComponent, FormularioTransacaoComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
  modal = viewChild(ModalComponent)
  transacao = {
    sucesso: true,
    data: {
        "totalSaldo": 5140.84,
        "totalReceita": 5490.74,
        "totalDespesa": 349.9
    },
    mensagens: []
}

  adicionar(event: any){
    this.modal()?.toggle(event)
  }
}
