import { FormGroup } from '@angular/forms';
import { IDisplayMessage, IValidationMessage } from './IValidationMessage';
export class FormValidationService {
  constructor(private mensagensValidacao: IValidationMessage) { }

  executeValidation(formGroup?: FormGroup): IDisplayMessage {
    if (!formGroup) return {};

    return this.validateFormGroup(formGroup, this.mensagensValidacao);
  }

  private validateFormGroup(formGroup: FormGroup, validationMessages: any): IDisplayMessage {
    return Object.keys(formGroup.controls).reduce((result, controlName) => {
      const formControl = formGroup.controls[controlName];
      const errors = formControl.errors;
      if (formControl instanceof FormGroup) {
        const childMessages = this.validateFormGroup(formControl, validationMessages);
        result = { ...result, ...childMessages };
      } else {
        if (validationMessages[controlName] && (formControl.dirty || formControl.touched) && errors) {
          result[controlName] = '';
          Object.keys(errors).forEach(errorKey => {
            if (validationMessages[controlName][errorKey]) {
              result[controlName] += validationMessages[controlName][errorKey] + '<br />';
            }
          });
        }
      }

      return result;
    }, {} as IDisplayMessage);
  }
}
