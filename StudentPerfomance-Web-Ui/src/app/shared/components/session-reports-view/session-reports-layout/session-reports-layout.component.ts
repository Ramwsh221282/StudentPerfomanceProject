import { Component, EventEmitter, Input, Output } from '@angular/core';
import { SessionReportItemComponent } from './session-report-item/session-report-item.component';
import { ControlWeekReportInterface } from '../Models/Data/control-week-report-interface';

@Component({
  selector: 'app-session-reports-layout',
  standalone: true,
  imports: [SessionReportItemComponent],
  templateUrl: './session-reports-layout.component.html',
  styleUrl: './session-reports-layout.component.scss',
})
export class SessionReportsLayoutComponent {
  @Input({ required: true }) reports: ControlWeekReportInterface[];
  @Output() refreshEmitter: EventEmitter<void> = new EventEmitter();
  @Output() successEmitter: EventEmitter<void> = new EventEmitter();
  @Output() failureEmitter: EventEmitter<void> = new EventEmitter();
}
