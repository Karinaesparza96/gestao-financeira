import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { TipoMensagem } from '../models/tipoMensagem';

@Injectable({
  providedIn: 'root'
})
export class NotificacaoService {

  private mensagemSubject = new BehaviorSubject<{mensagem: string, tipo: TipoMensagem} | null>(null);
  mensagem$ = this.mensagemSubject.asObservable();

  mostrarMensagem(mensagem: string,  tipo: TipoMensagem = 'sucesso', duracaoMs: number = 3000) {
    this.mensagemSubject.next({mensagem, tipo});

    setTimeout(() => {
      this.mensagemSubject.next(null);
    }, duracaoMs);
  }
}
