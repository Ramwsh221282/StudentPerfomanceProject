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
  selector: 'app-success-notification',
  imports: [NgIf],
  templateUrl: './success-notification.component.html',
  styleUrl: './success-notification.component.scss',
  standalone: true,
})
export class SuccessNotificationComponent implements OnInit, OnDestroy {
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
