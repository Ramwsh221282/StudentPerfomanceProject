import { Component, EventEmitter, Input, Output } from '@angular/core';
import { DepartmentStatisticsReportEntity } from '../../../../../../../models/report/department-statistics-report-entity';
import { DepartmentStatisticsReportPartEntity } from '../../../../../../../models/report/department-statistics-report-part-entity';

@Component({
  selector: 'app-department-report',
  templateUrl: './department-report.component.html',
  styleUrl: './department-report.component.scss',
})
export class DepartmentReportComponent {
  @Input({ required: true }) report: DepartmentStatisticsReportEntity;
  @Output() visibility: EventEmitter<void> = new EventEmitter();

  protected selectedDepartment: DepartmentStatisticsReportPartEntity;
  protected isTeachersReportVisible: boolean = false;
}
