import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ControlWeekReportEntity } from '../../../../../../models/report/control-week-report';

@Component({
  selector: 'app-assignment-session-report',
  templateUrl: './assignment-session-report.component.html',
  styleUrl: './assignment-session-report.component.scss',
})
export class AssignmentSessionReportComponent {
  @Input({ required: true }) report: ControlWeekReportEntity;
  @Output() visibility: EventEmitter<void> = new EventEmitter();

  protected isCourseReportVisible: boolean = false;
  protected isDirectionCodeReportVisible: boolean = false;
  protected isDepartmentReportVisible: boolean = false;
  protected isDirectionTypeReportVisible: boolean = false;
  protected isGroupsReportVisible: boolean = false;
}
