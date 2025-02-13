import { Routes } from '@angular/router';
import { FormularioTransacaoComponent } from './formulario-transacao/formulario-transacao.component';

export const routes: Routes = [
  {
    path: '', component: FormularioTransacaoComponent
  },
  {
    path: 'novo', component: FormularioTransacaoComponent
  },
  {
    path: 'editar/:id', component: FormularioTransacaoComponent
  },
  {
    path: 'detalhes/:id', component: FormularioTransacaoComponent
  },
  {
    path: 'deletar/:id', component: FormularioTransacaoComponent
  }
]
