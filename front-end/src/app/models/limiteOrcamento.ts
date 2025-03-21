export interface LimiteOrcamento {
  id: string
  categoriaId?: string
  categoriaNome?: string
  periodo: Date
  limite: number
  tipoLimite: TipoLimite
  porcentagemAviso: number
  limiteUtilizado?: number
  percentualLimiteUtilizado?: string
}

export enum TipoLimite
{
  geral = 1,
  categoria
}