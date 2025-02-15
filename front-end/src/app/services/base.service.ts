import { throwError } from 'rxjs';
import { HttpErrorResponse, HttpHeaders } from "@angular/common/http";

export abstract class BaseService {
  protected UrlService: string = "http://localhost:5224/api";

  protected ObterHeaderJson(){
    return {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
  }

  protected extractData(response: any){
    return response.data || {};
  }

  protected extractSucesso(response: any){
    return response.sucesso || {};
  }

  protected serviceError(response: Response | any){
    let customError: string[] = [];

    if (response instanceof HttpErrorResponse){
      if (response.statusText === "Unknown Error"){
        customError.push("Ocorreu um erro desconhecido");
        response.error.mensagens = customError;
      }
    }

    console.error(response);
    return throwError(response);
  }
}
