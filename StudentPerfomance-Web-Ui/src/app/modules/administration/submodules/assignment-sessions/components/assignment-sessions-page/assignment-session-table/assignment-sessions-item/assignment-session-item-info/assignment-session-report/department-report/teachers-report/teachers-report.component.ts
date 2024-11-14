import { Component, EventEmitter, Input, Output } from '@angular/core';
import { TeacherStatisticsReportPartEntity } from '../../../../../../../../models/report/teacher-statistics-report-part-entity';

@Component({
  selector: 'app-teachers-report',
  templateUrl: './teachers-report.component.html',
  styleUrl: './teachers-report.component.scss',
})
export class TeachersReportComponent {
  @Input({ required: true }) report: TeacherStatisticsReportPartEntity[];
  @Output() visibility: EventEmitter<void> = new EventEmitter();
}
