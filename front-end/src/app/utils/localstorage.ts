export class LocalStorageUtils {

  public ObterUsuario(){
    return localStorage.getItem('user');
  }

  public salvarDadosLocaisUsuario(response: any){
    this.salvarTokenUsuario(response.accessToken);
    this.salvarUsuario(response.userToken);
  }

  public limparDadosLocaisUsuario(){
    localStorage.removeItem('token');
    localStorage.removeItem('user');
  }

  public obterTokenUsuario(): string {
    return localStorage.getItem('token')!;
  }

  public salvarTokenUsuario(token: string){
    localStorage.setItem('token', token);
  }

  public salvarUsuario(user: string){
    localStorage.setItem('user', JSON.stringify(user));
  }
}
