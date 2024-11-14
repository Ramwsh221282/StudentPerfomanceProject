import { Component, EventEmitter, Input, Output } from '@angular/core';
import { StudentStatisticsPartEntity } from '../../../../../../../models/report/student-statistics-report';

@Component({
  selector: 'app-students-report',
  templateUrl: './students-report.component.html',
  styleUrl: './students-report.component.scss',
})
export class StudentsReportComponent {
  @Input({ required: true }) report: StudentStatisticsPartEntity[];
  @Output() visibility: EventEmitter<void> = new EventEmitter();
}
