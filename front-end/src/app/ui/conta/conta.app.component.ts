import { Component } from "@angular/core";
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-conta',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
    <div class="container">
      <router-outlet></router-outlet>
    </div>
  `
 })
 export class ContaAppComponent{  }
