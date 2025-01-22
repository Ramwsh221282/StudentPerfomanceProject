import {
  Component,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output,
} from '@angular/core';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-failure-notification',
  imports: [NgIf],
  templateUrl: './failure-notification.component.html',
  styleUrl: './failure-notification.component.scss',
  standalone: true,
})
export class FailureNotificationComponent implements OnInit, OnDestroy {
  @Output() visibilityChange: EventEmitter<void> = new EventEmitter<void>();
  @Input({ required: true }) message: string;
  public isVisible = true;
  private timeOutId: any;

  public ngOnInit() {
    this.timeOutId = setTimeout(() => {
      this.visibilityChange.emit();
      this.isVisible = false;
    }, 5000);
  }

  public ngOnDestroy() {
    clearTimeout(this.timeOutId);
  }
}
