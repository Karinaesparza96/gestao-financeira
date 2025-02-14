import { Component, OnDestroy, OnInit } from '@angular/core';

@Component({
  selector: 'app-saudacao-usuario',
  imports: [],
  templateUrl: './saudacao-usuario.component.html',
  styleUrl: './saudacao-usuario.component.scss'
})
export class SaudacaoUsuarioComponent implements OnInit, OnDestroy {
  saudacao: string = '';
  icone: string =  '';
  nomeUsuario: string = ''

  private saudacoes : any = {
    manha: {
      saudacao: 'Bom dia',
      icone: '☀️',
      range: [6, 12]
    },
    tarde: {
      saudacao: 'Boa tarde',
      icone: '🌤️',
      range: [12, 18]
    },
    noite: {
      saudacao: 'Boa noite',
      icone: '🌙',
      range: [18, 6]
    }
  }

  private cleanInterval: any

  constructor() {}

  ngOnInit(): void {
    this.setSaudacao()

    this.cleanInterval = setInterval(() => {
      this.setSaudacao()
    }, 50000);
  }

  ngOnDestroy(): void {
    if (this.cleanInterval) {
      clearInterval(this.cleanInterval)
    }
  }

  setSaudacao() {
    let hora = new Date().getHours();

    for(let key in this.saudacoes ) {
      const {saudacao, icone, range} = this.saudacoes[key]

      if (this.isInRange(hora, range)) {
        this.icone = icone;
        this.saudacao = saudacao
        break;
      }
    }
  }

  isInRange(hora: number, range: [number, number]) {
    const [inicio, fim] = range;

    return inicio < fim 
      ? hora >= inicio && hora < fim
      : hora >= inicio || hora < fim;
  }
}
