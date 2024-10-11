import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-success-result-notification',
  standalone: true,
  imports: [],
  templateUrl: './success-result-notification.component.html',
  styleUrl: './success-result-notification.component.scss',
})
export class SuccessResultNotificationComponent {
  @Input({ required: true }) message: string;
  @Output() visibilityEmitter: EventEmitter<boolean> =
    new EventEmitter<boolean>();

  protected close(): void {
    this.visibilityEmitter.emit(false);
  }
}
