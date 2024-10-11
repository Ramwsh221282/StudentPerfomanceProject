import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-failure-result-notification',
  standalone: true,
  imports: [],
  templateUrl: './failure-result-notification.component.html',
  styleUrl: './failure-result-notification.component.scss',
})
export class FailureResultNotificationComponent {
  @Input({ required: true }) message: string;
  @Output() visibilityEmitter: EventEmitter<boolean> =
    new EventEmitter<boolean>();

  protected close(): void {
    this.visibilityEmitter.emit(false);
  }
}
