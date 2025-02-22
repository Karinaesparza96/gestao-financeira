import { ElementRef, QueryList } from "@angular/core"
import { IDisplayMessage, IValidationMessage } from "../utils/validation/IValidationMessage"
import { fromEvent, merge } from "rxjs"
import { FormValidationService } from "../utils/validation/form-validation.service"
import { FormGroup } from "@angular/forms"

export abstract class BaseFormComponent {
  erros: IDisplayMessage = {}
  errosServer: string[] = []
  modoAlteracao: boolean = false
  formValidation!: FormValidationService

  constructor() { }

  protected configurarMensagensValidacao(validationMessages: IValidationMessage) {
    this.formValidation = new FormValidationService(validationMessages);
  }

  protected validateForm(formGroup:FormGroup, formControls: QueryList<ElementRef>) {
    merge(...this.initializeBlurEvents(formControls)).subscribe(() => {
      this.erros = this.executeValidation(formGroup)
    })
  }

  protected executeValidation(formGroup:FormGroup) {
    return this.formValidation.executeValidation(formGroup)
  }
  private initializeBlurEvents(formControls: QueryList<ElementRef>) {
    return (formControls ?? []).map((fc: ElementRef) => fromEvent(fc.nativeElement, 'blur'))
  }

}