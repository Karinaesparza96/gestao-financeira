import { ElementRef } from "@angular/core"
import { IDisplayMessage, IValidationMessage } from "../utils/validation/IValidationMessage"
import { FormControlName, FormGroup } from "@angular/forms"
import { fromEvent, merge, Observable } from "rxjs"
import { FormValidationService } from "../utils/validation/form-validation.service"

export abstract class BaseFormComponent {
  erros: IDisplayMessage = {}
  errosServer: string[] = []
  modoAlteracao: boolean = false
  formValidation!: FormValidationService

  constructor() { }

  protected configurarMensagensValidacaoBase(validationMessages: IValidationMessage) {
    this.formValidation = new FormValidationService(validationMessages);
  }

  protected validateForm(formGroup: FormGroup, formControls: ElementRef[]) {
    merge(...this.initializeBlurEvents(formControls)).subscribe(() => {
      this.erros = this.formValidation.executeValidation(formGroup)
    })
  }

  private initializeBlurEvents(formControls: ElementRef[]) {
    return (formControls ?? []).map((fc: ElementRef) => fromEvent(fc.nativeElement, 'blur'))
  }

}