import { TipoTransacao } from "./TipoTransacao";


export class Transacao {
  id?: string;
  descricao: string = '';
  categoriaId: string = '';
  tipo: TipoTransacao = 1;
  valor: number = 0;
  data: Date = new Date();

  constructor(init?: Partial<Transacao>) {
    Object.assign(this, init);
  }

}
