import { Component, OnInit} from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { Usuario } from '../../../models/usuario';
import { ContaService } from '../../../services/conta.service';
import { ErrorListComponent } from '../../../components/error-list/error-list.component';


@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule, ErrorListComponent],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  errors: any[] = [];
  usuario!: Usuario;
  defaultUrl: string = '/home';

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private contaService: ContaService
  ) {
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      senha: ['', [Validators.required]]
    });
  }

  ngOnInit(): void {
    if (this.contaService.LocalStorage.ObterUsuario()) {
      this.router.navigate(['/home']);
    }
    this.defaultUrl = this.route.snapshot.queryParams['returnUrl'] ?? this.defaultUrl;
  }

  onSubmit() {
    if (this.loginForm.valid) {
      this.usuario = Object.assign({}, this.usuario, this.loginForm.value);

      this.contaService.login(this.usuario)
      .subscribe(
        sucesso => {this.processarSucesso(sucesso)},
        falha => {this.processarFalha(falha)}
      );
    }
  }

  processarSucesso(response: any){
    this.loginForm.reset();
    this.errors = [];
    this.contaService.LocalStorage.salvarDadosLocaisUsuario(response);
    this.router.navigateByUrl(this.defaultUrl);
  }

  processarFalha(fail: any){
    this.errors = fail.error.mensagens;
  }
}
