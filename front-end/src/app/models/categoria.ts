
export interface Categoria {
  id: string
  nome: string
  default: boolean
}
export type NovaCategoria = Omit<Categoria, 'id'>;
