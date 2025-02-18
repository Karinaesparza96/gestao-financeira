import { Component } from '@angular/core';
import { NotificacaoService } from '../../utils/notificacao.service';
import { CommonModule } from '@angular/common';
import { TipoMensagem } from '../../models/tipoMensagem';

@Component({
  selector: 'app-notificacao',
  imports: [CommonModule],
  templateUrl: './notificacao.component.html',
  styleUrl: './notificacao.component.scss'
})
export class NotificacaoComponent {
  option: {mensagem: string, tipo: TipoMensagem} | null = null;
  constructor(private notificacaoService: NotificacaoService) {}

  ngOnInit() {
    this.notificacaoService.mensagem$.subscribe(notificacao => {
      this.option = notificacao;
    });
  }
}
