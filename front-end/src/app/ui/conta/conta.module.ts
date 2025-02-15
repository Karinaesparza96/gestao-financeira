import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegisterComponent } from './register/register.component';
import { RouterModule } from '@angular/router';
import { ContaRoutingModule } from './conta.route';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { ContaAppComponent } from './conta.app.component';
import { LoginComponent } from './login/login.component';
import { ContaService } from '../../services/conta.service';



@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    ContaRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    ContaAppComponent,
    LoginComponent,
    RegisterComponent,

  ],
  providers:[
    ContaService
  ]
})
export class ContaModule { }
