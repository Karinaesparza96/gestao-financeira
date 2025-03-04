import { throwError } from 'rxjs';
import { HttpErrorResponse, HttpHeaders } from "@angular/common/http";
import { LocalStorageUtils } from '../utils/localstorage';

export abstract class BaseService {
  public LocalStorage = new LocalStorageUtils();
  protected UrlService: string = "http://localhost:5224/api";

  protected ObterHeaderJson(){
    return {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
  }

  protected ObterAuthHeaderJson(){
    return {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${this.LocalStorage.obterTokenUsuario()}`
      })
    }
  }

  protected extractData(response: any){
    return response?.data || [];
  }

  protected extractSucesso(response: any){
    return response?.sucesso || {};
  }

  protected extractMensagens(response: any){
    return response?.mensagens || [];
  }

  protected serviceError(response: Response | any) {
    let customError: string[] = [];
    let customResponse: { error: { mensagens: string[] }} = { error: { mensagens: [] }}

    if (response instanceof HttpErrorResponse) {

        if (response.statusText === "Unknown Error") {
            customError.push("Ocorreu um erro desconhecido");
            response.error.mensagens = customError;
        }
    }
    if (response.status === 500) {
        customError.push("Ocorreu um erro no processamento, tente novamente mais tarde ou contate o nosso suporte.");
                    
        customResponse.error.mensagens = customError;
        return throwError(() => customResponse);
    }

    console.error(response);
    return throwError(() => response);
}
}
