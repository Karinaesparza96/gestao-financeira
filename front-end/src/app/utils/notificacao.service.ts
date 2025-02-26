import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { TipoMensagem } from '../models/tipoMensagem';

@Injectable({
  providedIn: 'root'
})
export class NotificacaoService {
  private mensagemSubject = new BehaviorSubject<{mensagem: string, tipo: TipoMensagem} | null>(null);
  mensagem$ = this.mensagemSubject.asObservable();

  show(mensagem: string,  tipo: TipoMensagem = 'sucesso') {
    this.mensagemSubject.next({mensagem, tipo});
  }
}
