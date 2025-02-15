import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Usuario } from "../models/usuario";
import { catchError, map, Observable } from "rxjs";
import { BaseService } from "./base.service";

@Injectable()
export class ContaService extends BaseService{
  constructor(private http: HttpClient){ super(); }

  registrarUsuario(usuario: Usuario) : Observable<Usuario>{
    let response = this.http
        .post(this.UrlService + '/conta/registrar', usuario, this.ObterHeaderJson())
        .pipe(
           map(this.extractSucesso),
           catchError(this.serviceError));

    return response;
  }

  login(usuario: Usuario){

  }
}
