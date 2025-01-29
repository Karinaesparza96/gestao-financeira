import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.scss'],
  imports: [CommonModule]
})
export class ModalComponent {
  @Input({ required: true }) label!: string;
  showModal: boolean = false;

  toggle(event: PointerEvent | MouseEvent) {
    if (event.currentTarget != event.target) return;
    this.showModal = !this.showModal;
  }

}
