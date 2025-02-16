export class FormDataUtils {

  static formatarValor(valor: any): string {
    console.log(valor)
    if (typeof valor == 'string' && valor.includes(',')) {
      return valor.replace(',', '.')
    }
    return valor
  }
}