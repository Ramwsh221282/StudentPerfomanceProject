import { Component, EventEmitter, Input, Output } from '@angular/core';
import { GroupStatisticsReportEntity } from '../../../../../../../models/report/group-statistics-report';

@Component({
  selector: 'app-group-report',
  templateUrl: './group-report.component.html',
  styleUrl: './group-report.component.scss',
})
export class GroupReportComponent {
  @Input({ required: true }) report: GroupStatisticsReportEntity[];
  @Output() visibility: EventEmitter<void> = new EventEmitter();

  protected selectedGroup: GroupStatisticsReportEntity;
  protected isGroupStatisticsVisible: boolean = false;
}
