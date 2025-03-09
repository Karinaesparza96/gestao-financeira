import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ContaService } from '../../../services/conta.service';
import { Usuario } from '../../../models/usuario';
import { ErrorListComponent } from '../../../components/error-list/error-list.component';


@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule, ErrorListComponent],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  registerForm: FormGroup;
  usuario!: Usuario;

  errors: any[] = [];


  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private contaService: ContaService
  ) {
    this.registerForm = this.formBuilder.group({
      nome: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      senha: ['', [Validators.required, Validators.minLength(8)]],
      confimacaoSenha: ['', Validators.required]
    }, { validator: this.passwordMatchValidator });
  }

  passwordMatchValidator(g: FormGroup) {
    return g.get('senha')?.value === g.get('confimacaoSenha')?.value
      ? null : { 'mismatch': true };
  }

  onSubmit() {
    if (this.registerForm.valid) {
      this.usuario = Object.assign({}, this.usuario, this.registerForm.value);

      this.contaService.registrarUsuario(this.usuario).subscribe(
        sucesso => {this.processarSucesso(sucesso)},
        falha => {this.processarFalha(falha)}
      );
    }
  }

  processarSucesso(response: any){
    this.registerForm.reset();
    this.errors = [];
    this.router.navigate(['/conta/login']);
  }

  processarFalha(fail: any){
    this.errors = fail.error.mensagens;
  }
}
