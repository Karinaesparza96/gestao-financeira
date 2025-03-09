import { Component, OnInit, OnDestroy} from '@angular/core';
import { CommonModule } from '@angular/common';
import { CategoriaService } from '../../services/categoria.service';
import { Categoria, NovaCategoria } from '../../models/categoria';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { Subject, takeUntil, finalize } from 'rxjs';
import { NotificacaoService } from '../../utils/notificacao.service';

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

  constructor(
    private categoriaService: CategoriaService,
    private notificacao: NotificacaoService
  ) {}

  ngOnInit(): void {
    this.carregarCategorias();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  carregarCategorias(): void {
    this.carregando = true;
    this.erro = null;

    this.categoriaService.obterTodos()
      .pipe(
        takeUntil(this.destroy$),
        finalize(() => this.carregando = false)
      )
      .subscribe(data => {
        this.categorias = data;
      });
  }

  iniciarNovaCategoria(): void {
    this.erro = null;
    this.categoriaEmEdicao = {
      nome: '',
      default: false
    } as Categoria;
    this.ehNovaCategoria = true;
    this.modoEdicao = true;
  }

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
        finalize(() => this.carregando = false)
      ) 
      .subscribe({
        next: () => this.processarSucesso(),
        error: (error) => this.processarFalha(error)
      });
  }

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
        finalize(() => this.carregando = false)
      )
      .subscribe({
        next: () => this.processarSucesso(),
        error: (error) => this.processarFalha(error)
      });
  }

  cancelarEdicao(): void {
    this.modoEdicao = false;
    this.ehNovaCategoria = false;
    this.categoriaEmEdicao = {} as Categoria;
    this.erro = null;
  }

  private processarSucesso() {
    this.cancelarEdicao();
    this.carregarCategorias();
    this.notificacao.show('Operação realizada com sucesso!');
  }

  private processarFalha(fail: any): void {
    const erros = fail.error.mensagens.join('<br />');
    this.notificacao.show(erros, 'falha');
  }
}
