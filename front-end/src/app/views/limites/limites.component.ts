import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { LimiteOrcamento } from '../../models/limiteOrcamento';
import { LimiteService } from '../../services/limite.service';
import { FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-limites',
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './limites.component.html',
  styleUrl: './limites.component.scss'
})
export class LimitesComponent implements OnInit{
  limites: LimiteOrcamento[] = []
  limiteOrcamentoForm!: FormGroup
  constructor(private limiteService: LimiteService) {}


  ngOnInit(): void {
    this.limiteService.obterTodos().subscribe((r) => this.limites = r)
  }

  submit(){}
}
