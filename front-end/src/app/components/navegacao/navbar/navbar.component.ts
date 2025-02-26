import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { ContaService } from '../../../services/conta.service';

@Component({
  selector: 'app-navbar',
  imports: [CommonModule, RouterModule],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent {
  isMenuOpen = false;

  constructor(private router: Router,
    private contaService: ContaService) { }
  routes = [
    { linkName: 'Home', url: '/home', exact: true, classActive: 'ativo' },
    { linkName: 'Categorias', url: '/categorias', exact: true, classActive: 'ativo' },
    { linkName: 'Transações', url: '/transacoes', exact: true, classActive: 'ativo' },
    { linkName: 'Limites', url: '/limites', exact: true, classActive: 'ativo' },
    { linkName: 'Relatórios', url: '/relatorios', exact: true, classActive: 'ativo' },
  ];
  rotasConta = [
    { linkName: 'Entrar', url: '/conta/login', exact: false, classActive: 'ativo' },
    { linkName: 'Registrar', url: '/conta/register', exact: false, classActive: 'ativo' },
  ]
  toggleMenu() {
    this.isMenuOpen = !this.isMenuOpen;
  }

  isLoggedIn(): boolean {
    return !!this.contaService.LocalStorage.ObterUsuario();
  }

  getUserEmail(): string {
    const userData = this.contaService.LocalStorage.ObterUsuario();
    if (userData) {
      return JSON.parse(userData).email;
    }
    return '';
  }

  logout(event: Event) {
    event.preventDefault();
    this.contaService.LocalStorage.limparDadosLocaisUsuario();
    this.router.navigate(['/conta/login']);
  }
}
