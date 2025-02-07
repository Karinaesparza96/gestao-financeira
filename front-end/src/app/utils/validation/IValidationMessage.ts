export interface IValitionContainerMessage {
  [key: string]: IValidationMessage
}

export interface IValidationMessage {
  [key: string] : {[key: string]: string}
}

export interface IDisplayMessage {
  [key: string] : string
}