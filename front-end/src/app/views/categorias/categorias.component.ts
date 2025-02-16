import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CategoriaService } from '../../services/categoria.service';
import { Categoria, NovaCategoria } from '../../models/categoria';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { Subject, takeUntil, catchError, finalize } from 'rxjs';

@Component({
  selector: 'app-categorias',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './categorias.component.html',
  styleUrl: './categorias.component.scss'
})
export class CategoriasComponent implements OnInit, OnDestroy {
  categorias: Categoria[] = [];
  categoriaEmEdicao: Categoria = {} as Categoria;
  modoEdicao = false;
  ehNovaCategoria = false;
  carregando = false;
  erro: string | null = null;

  private destroy$ = new Subject<void>();

  constructor(private categoriaService: CategoriaService) {
    console.log('Componente Categorias construído');
  }

  ngOnInit(): void {
    console.log('Iniciando carregamento das categorias...');
    this.carregarCategorias();
  }

  // Destruir o componente
  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  // Carregar categorias
  carregarCategorias(): void {
    this.carregando = true;
    this.erro = null;

    this.categoriaService.obterTodos()
      .pipe(
        takeUntil(this.destroy$),
        catchError(error => {
          this.erro = 'Erro ao carregar categorias. Por favor, tente novamente.';
          console.error('Erro ao carregar categorias:', error);
          return []; // Retorna array vazio em caso de erro
        }),
        finalize(() => this.carregando = false)
      )
      .subscribe(data => {
        this.categorias = data;
      });
  }

  // Iniciar nova categoria
  iniciarNovaCategoria(): void {
    this.erro = null;
    this.categoriaEmEdicao = {
      nome: '',
      default: false
    } as Categoria;
    this.ehNovaCategoria = true;
    this.modoEdicao = true;
  }

  // Editar categoria
  editarCategoria(categoria: Categoria): void {
    this.erro = null;
    this.categoriaEmEdicao = { ...categoria };
    this.ehNovaCategoria = false;
    this.modoEdicao = true;
  }

  salvarCategoria(): void {
    if (!this.categoriaEmEdicao.nome.trim()) {
      this.erro = 'O nome da categoria é obrigatório.';
      return;
    }

    this.carregando = true;
    this.erro = null;

    const categoriaPraSalvar: NovaCategoria = {
      nome: this.categoriaEmEdicao.nome,
      default: this.categoriaEmEdicao.default
    };

    const operacao = this.ehNovaCategoria
      ? this.categoriaService.novaCategoria(categoriaPraSalvar)
      : this.categoriaService.atualizarCategoria(this.categoriaEmEdicao);

    operacao
      .pipe(
        takeUntil(this.destroy$),
        catchError(error => {
          this.erro = this.ehNovaCategoria
            ? 'Erro ao criar categoria. Por favor, tente novamente.'
            : 'Erro ao atualizar categoria. Por favor, tente novamente.';
          if (error.error?.mensagens?.length > 0) {
            this.erro = error.error.mensagens.join(', ');
          }
          console.error('Erro ao salvar categoria:', error);
          throw error;
        }),
        finalize(() => this.carregando = false)
      )
      .subscribe({
        next: (categorias) => {
          this.categorias = categorias;
          this.cancelarEdicao();
          this.carregarCategorias();
        },
        error: (error) => {
          console.error('Falha na operação:', error)
          this.erro = 'Ocorreu um erro ao processar a operação. Por favor, tente novamente.'
        }
      });
  }

  // Excluir categoria
  excluirCategoria(id: string): void {
    if (!id) {
      this.erro = 'ID da categoria inválido';
      return;
    }

    const categoria = this.categorias.find(c => c.id === id);
    if (categoria?.default) {
      this.erro = 'Não é possível excluir uma categoria padrão';
      return;
    }

    if (!confirm('Tem certeza que deseja excluir esta categoria?')) {
      return;
    }

    this.carregando = true;
    this.erro = null;

    this.categoriaService.excluirCategoria(id)
      .pipe(
        takeUntil(this.destroy$),
        catchError(error => {
          this.erro = 'Erro ao excluir categoria. Por favor, tente novamente.';
          if (error.error?.mensagens?.length > 0) {
            this.erro = error.error.mensagens.join(', ');
          }
          console.error('Erro ao excluir categoria:', error);
          throw error;
        }),
        finalize(() => this.carregando = false)
      )
      .subscribe({
        next: (categorias) => {
          this.categorias = categorias;
          this.cancelarEdicao();
          this.carregarCategorias();
        },
        error: (error) => {
          console.error('Falha na operação:', error)
          this.erro = 'Ocorreu um erro ao processar a operação. Por favor, tente novamente.'
        }
      });
  }

  // Cancelar edição
  cancelarEdicao(): void {
    this.modoEdicao = false; // Desativa o modo de edição // Desativa o modo de edição
    this.ehNovaCategoria = false; // Desativa a flag de nova categoria // Desativa a flag de nova categoria
    this.categoriaEmEdicao = {} as Categoria; // Reseta a categoria em edição // Reseta a categoria em edição
    this.erro = null; // Reseta o erro // Reseta o erro
  }
}
