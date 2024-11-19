import { Component, Input } from '@angular/core';
import { ControlWeekReportInterface } from '../../Models/Data/control-week-report-interface';
import { DatePipe } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-session-report-item',
  standalone: true,
  imports: [DatePipe, RouterLink],
  templateUrl: './session-report-item.component.html',
  styleUrl: './session-report-item.component.scss',
})
export class SessionReportItemComponent {
  @Input({ required: true }) report: ControlWeekReportInterface;

  public constructor(protected readonly date: DatePipe) {}
}
