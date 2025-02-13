import { TipoTransacao } from "./TipoTransacao";


export class Transacao {
  id?: string;
  descricao: string = '';
  categoriaId: string = '';
  tipo: TipoTransacao = 1;
  valor: number = 0;
  data: string = new Date().toLocaleDateString();

}
