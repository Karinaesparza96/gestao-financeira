import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ValidationMessageService } from './validation-message.service';
import { IDisplayMessage } from './IValidationMessage';

@Injectable({
  providedIn: 'root'
})
export class FormValidationService {
  constructor(private validationMessage: ValidationMessageService) { }

  executeValidation(formGroup: FormGroup, formName: string) {
    const validationMessages = this.validationMessage.getMessages(formName)

    return Object.keys(formGroup.controls).reduce((result, itemAtual) => {
      const formControl = formGroup.controls[itemAtual] 
      const erros = formControl.errors
      if (formControl instanceof FormGroup) {
        let childMessages = this.executeValidation(formControl, itemAtual);
        result = { ...result, ...childMessages };
      } else {
        if (validationMessages[itemAtual] && (formControl.dirty || formControl.touched) && erros) {
          result[itemAtual] = ''
          Object.keys(erros).map( (errorKey) => {
            if (validationMessages[itemAtual][errorKey]) {
              result[itemAtual] += validationMessages[itemAtual][errorKey] + '<br />'
            }
          })
        }
      }
      console.log(result)
      return result
    }, {} as IDisplayMessage)
  }
}
