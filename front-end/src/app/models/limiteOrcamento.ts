import { Categoria } from "./categoria"

export interface LimiteOrcamento {
  id: string
  categoriaId?: string
  periodo: Date
  limite: number
  tipoLimite: TipoLimite
  porcentagemAviso: number
  categoria?: Categoria
}

export enum TipoLimite
{
  geral = 1,
  categoria
}