import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { ContaAppComponent } from "./conta.app.component";
import { RegisterComponent } from "./register/register.component";
import { LoginComponent } from "./login/login.component";

const contaRouterConfig: Routes = [
  {
    path: '', component: ContaAppComponent,
    children: [
      { path: '', redirectTo: 'login', pathMatch: 'full' },
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent }
    ]
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(contaRouterConfig)
  ],
  exports: [RouterModule]
})
export class ContaRoutingModule {}
