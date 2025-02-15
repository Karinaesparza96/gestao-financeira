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
              private contaService: ContaService) {}

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
    this.router.navigate(['/']);
  }
}
