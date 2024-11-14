import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AssignmentSession } from '../../../../../models/assignment-session-interface';
import { AssignmentSessionWeek } from '../../../../../models/assignment-session-week';
import { ControlWeekReportEntity } from '../../../../../models/report/control-week-report';
import { AssignmentSessionDataService } from '../../assignment-session-data.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-assignment-session-item-info',
  templateUrl: './assignment-session-item-info.component.html',
  styleUrl: './assignment-session-item-info.component.scss',
  providers: [DatePipe],
})
export class AssignmentSessionItemInfoComponent implements OnInit {
  @Input({ required: true }) session: AssignmentSession;
  @Output() visibility: EventEmitter<void> = new EventEmitter();
  protected isAssignmentsVisible: boolean = false;
  protected selectedAssignmentWeek: AssignmentSessionWeek;

  protected isByUniversityVisible: boolean = false;
  protected isByDepartmentVisible: boolean = false;
  protected isByCourseVisible: boolean = false;
  protected isByDirectionsVisible: boolean = false;
  protected isByDirectionsCodeVisible: boolean = false;

  protected report: ControlWeekReportEntity | null;
  protected isReportModalVisible: boolean = false;

  public constructor(
    private readonly service: AssignmentSessionDataService,
    private readonly _datePipe: DatePipe,
  ) {
    this.report == null;
  }

  public ngOnInit(): void {
    this.service.getReport(this.session).subscribe((response) => {
      this.report = response;
      if (this.report != null) {
        this.report.creationDate = this._datePipe.transform(
          this.report.creationDate,
          'dd-MM-yyyy',
        )!;
        this.report.completionDate = this._datePipe.transform(
          this.report.completionDate,
          'dd-MM-yyyy',
        )!;
      }
    });
  }
}
