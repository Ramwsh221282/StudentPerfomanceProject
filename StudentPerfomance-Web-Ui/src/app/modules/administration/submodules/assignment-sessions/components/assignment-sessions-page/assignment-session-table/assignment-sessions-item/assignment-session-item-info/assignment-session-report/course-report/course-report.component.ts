import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CourseReportEntity } from '../../../../../../../models/report/course-report-entity';

@Component({
  selector: 'app-course-report',
  templateUrl: './course-report.component.html',
  styleUrl: './course-report.component.scss',
})
export class CourseReportComponent {
  @Input({ required: true }) courseReport: CourseReportEntity;
  @Output() visibility: EventEmitter<void> = new EventEmitter();
}
