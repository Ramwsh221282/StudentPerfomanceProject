import { Component, Input } from '@angular/core';
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
}
