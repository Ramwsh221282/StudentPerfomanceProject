import { Component, EventEmitter, Output } from '@angular/core';
import { RedOutlineButtonComponent } from '../../../../../building-blocks/buttons/red-outline-button/red-outline-button.component';

@Component({
  selector: 'app-completed-assignments-notification',
  imports: [RedOutlineButtonComponent],
  templateUrl: './completed-assignments-notification.component.html',
  styleUrl: './completed-assignments-notification.component.scss',
  standalone: true,
})
export class CompletedAssignmentsNotificationComponent {
  @Output() visibilityChanged: EventEmitter<void> = new EventEmitter();
}
