import { Injectable } from '@angular/core';
import { IValidationMessage, IValitionContainerMessage } from './IValidationMessage';

@Injectable({
  providedIn: 'root'
})
export class ValidationMessageService {
  private  messages: IValitionContainerMessage = {
    transacao : {
      descricao: {
        required: "O campo descrição é obrigatório."
      },
      valor: {
        required: "O campo valor é obrigatório."
      },
      data: {
        required: "O campo data é obrigatório."
      },
      tipo: {
        required: "O campo tipo é obrigatório."
      }
    },
    categoria: {
      id: {
        required: "O campo categoria é obrigatório."
      }
    },
  }

  getMessages(key: string) : IValidationMessage {
    return this.messages[key] ?? {}
  }
}
