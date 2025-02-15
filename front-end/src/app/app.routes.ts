import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { authGuard } from './guards/auth.guard';

export const routes: Routes = [
  {
    path: '',
    redirectTo: '/conta/login',
    pathMatch: 'full'
  },
  {
    path: 'conta',
    loadChildren: () => import('./ui/conta/conta.module').then(m => m.ContaModule)
  },
  {
    path: 'home',
    loadComponent: () => import('./views/visao-geral/home.component').then(m => m.HomeComponent),
    canActivate: [authGuard]
  }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutes {}

