import { Categoria } from "./categoria";
import { TipoTransacao } from "./TipoTransacao";


export class Transacao {
  id?: string;
  descricao: string = '';
  categoria?: Categoria;
  tipo: TipoTransacao = 1;
  valor: number = 0;
  data: string = new Date().toLocaleDateString();
}
