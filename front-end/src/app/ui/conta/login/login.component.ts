import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  loginForm: FormGroup;
  loginError: string = '';

  // Dados mockados para teste
  private mockUsers = [
    { email: 'usuario@teste.com', password: '123456' }
  ];

  constructor(
    private formBuilder: FormBuilder,
    private router: Router
  ) {
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });

    // Se já estiver logado, redireciona para home
    if (localStorage.getItem('user')) {
      this.router.navigate(['/home']);
    }
  }

  onSubmit() {
    if (this.loginForm.valid) {
      const { email, password } = this.loginForm.value;

      const user = this.mockUsers.find(u =>
        u.email === email && u.password === password
      );

      if (user) {
        localStorage.setItem('user', JSON.stringify({ email: user.email }));
        this.router.navigate(['/home']);
      } else {
        this.loginError = 'Email ou senha inválidos';
      }
    }
  }
}
