import { Component, EventEmitter, Output } from '@angular/core';
import { RedOutlineButtonComponent } from '../../../../building-blocks/buttons/red-outline-button/red-outline-button.component';

@Component({
  selector: 'app-admin-assignments-not-completed-popup',
  imports: [RedOutlineButtonComponent],
  templateUrl: './admin-assignments-not-completed-popup.component.html',
  styleUrl: './admin-assignments-not-completed-popup.component.scss',
  standalone: true,
})
export class AdminAssignmentsNotCompletedPopupComponent {
  @Output() visibilityChanged: EventEmitter<void> = new EventEmitter();
}
