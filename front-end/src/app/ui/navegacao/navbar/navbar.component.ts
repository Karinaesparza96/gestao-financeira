import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-navbar',
  imports: [CommonModule, RouterModule],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent {
  isMenuOpen = false;

  constructor(private router: Router) {}

  toggleMenu() {
    this.isMenuOpen = !this.isMenuOpen;
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('user');
  }

  getUserEmail(): string {
    const userData = localStorage.getItem('user');
    if (userData) {
      return JSON.parse(userData).email;
    }
    return '';
  }

  logout(event: Event) {
    event.preventDefault();
    localStorage.removeItem('user');
    this.router.navigate(['/']);
  }
}
