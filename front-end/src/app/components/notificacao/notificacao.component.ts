import { Component } from '@angular/core';
import { NotificacaoService } from '../../utils/notificacao.service';
import { CommonModule } from '@angular/common';
import { TipoMensagem } from '../../models/tipoMensagem';

@Component({
  selector: 'app-notificacao',
  imports: [CommonModule],
  templateUrl: './notificacao.component.html',
  styleUrl: './notificacao.component.scss',
})
export class NotificacaoComponent {
  option: { mensagem: string; tipo: TipoMensagem } | null = null;
  show = false;
  idTimeout: any;
  constructor(private notificacaoService: NotificacaoService) {}

  ngOnInit() {
    this.notificacaoService.mensagem$.subscribe((notificacao) => {
      if (!notificacao) return;
      this.option = notificacao;
      this.show = true;
      this.idTimeout = setTimeout(() => {
        this.show = false;
      }, 5000);
    });
  }

  onmouseenter() {
    clearTimeout(this.idTimeout);
  }

  onmouseleave() {
    setTimeout(() => {
      this.show = false;
    }, 1000);
  }

  fechar() {
    this.show = false;
  }
}
