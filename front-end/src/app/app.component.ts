import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavbarComponent } from "./components/navegacao/navbar/navbar.component";
import { NotificacaoComponent } from "./components/notificacao/notificacao.component";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, NavbarComponent, NotificacaoComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'Gestão Financeira';
}
