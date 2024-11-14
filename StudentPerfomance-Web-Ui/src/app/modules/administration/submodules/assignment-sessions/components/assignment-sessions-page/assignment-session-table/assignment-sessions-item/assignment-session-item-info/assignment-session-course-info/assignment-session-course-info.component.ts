import { Component, EventEmitter, Input, Output } from '@angular/core';
import { AssignmentSessionCoursePerfomance } from '../../../../../../models/assignment-session-course-perfomance';

@Component({
  selector: 'app-assignment-session-course-info',
  templateUrl: './assignment-session-course-info.component.html',
  styleUrl: './assignment-session-course-info.component.scss',
})
export class AssignmentSessionCourseInfoComponent {
  @Output() visibility: EventEmitter<void> = new EventEmitter();
  @Input({ required: true })
  coursePerfomances: AssignmentSessionCoursePerfomance[];
}
