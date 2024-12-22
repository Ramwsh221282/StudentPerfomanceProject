import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ControlWeekReportInterface } from '../../Models/Data/control-week-report-interface';
import { DatePipe, NgIf } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { SessionReportItemRemoveModalComponent } from './session-report-item-remove-modal/session-report-item-remove-modal.component';
import { BlueButtonComponent } from '../../../../../building-blocks/buttons/blue-button/blue-button.component';
import { RedButtonComponent } from '../../../../../building-blocks/buttons/red-button/red-button.component';

@Component({
  selector: 'app-session-report-item',
  standalone: true,
  imports: [
    DatePipe,
    RouterLink,
    SessionReportItemRemoveModalComponent,
    NgIf,
    BlueButtonComponent,
    RedButtonComponent,
  ],
  templateUrl: './session-report-item.component.html',
  styleUrl: './session-report-item.component.scss',
})
export class SessionReportItemComponent {
  @Input({ required: true }) report: ControlWeekReportInterface;
  @Output() removeRequested: EventEmitter<ControlWeekReportInterface> =
    new EventEmitter();

  public constructor(
    protected readonly date: DatePipe,
    private readonly _router: Router,
  ) {}

  protected navigateToGroupStatistics(): void {
    const Id = this.report.id;
    const startDate = this.report.creationDate;
    const endDate = this.report.completionDate;
    const season = this.report.season;
    const number = this.report.number;
    this._router.navigate(['/group-reports/', Id], {
      queryParams: {
        reportId: Id,
        startDate: startDate,
        endDate: endDate,
        season: season,
        number: number,
      },
    });
  }

  protected navigateToCourseStatistics(): void {
    const id = this.report.id;
    const startDate = this.report.creationDate;
    const endDate = this.report.completionDate;
    const season = this.report.season;
    const number = this.report.number;
    this._router.navigate(['/course-reports/', id], {
      queryParams: {
        reportId: id,
        startDate: startDate,
        endDate: endDate,
        season: season,
        number: number,
      },
    });
  }
}
