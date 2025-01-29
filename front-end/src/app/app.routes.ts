import { Routes } from '@angular/router';
import { HomeComponent } from './views/visao-geral/home.component';

export const routes: Routes = [
  {
    path: '', component: HomeComponent
  },
  // {
  //   path: 'conta', 
  //   children: [
  //     {
  //       path: 'login'
  //     },
  //     {
  //       path: 'registrar'
  //     }
  //   ]
  // },
  // {
  //   path: 'transacoes', 
  //   children: [
  //     {
  //       path: 'todos'
  //     },
  //     {
  //       path: 'novo'
  //     },
  //     {
  //       path: 'editar'
  //     },
  //     {
  //       path: 'detalhes/:id'
  //     }
  //   ]
  // },
  // {
  //   path: 'limites', 
  //   children: [
  //     {
  //       path: 'todos'
  //     },
  //     {
  //       path: 'novo'
  //     },
  //     {
  //       path: 'editar'
  //     },
  //     {
  //       path: 'detalhes/:id'
  //     }
  //   ]
  // }
];
