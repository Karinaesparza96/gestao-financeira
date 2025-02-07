import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.scss'],
  imports: [CommonModule]
})
export class ModalComponent {
  @Input() label: string = '';
  @Input() cssClass: string = '';
  @Input() showModal: boolean = false;
  @Output() modalChanged = new EventEmitter<boolean>();

  toggle(event: PointerEvent | MouseEvent) {
    if (event.currentTarget != event.target) return;
    
    this.showModal = !this.showModal;
    this.modalChanged.emit(this.showModal)
  }

}
