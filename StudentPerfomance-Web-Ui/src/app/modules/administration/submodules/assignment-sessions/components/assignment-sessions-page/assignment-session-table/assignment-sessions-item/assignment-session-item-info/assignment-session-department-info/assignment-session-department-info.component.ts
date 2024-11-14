import { Component, EventEmitter, Input, Output } from '@angular/core';
import { AssignmentSessionDepartmentPerfomance } from '../../../../../../models/assignment-session-department-perfomance';

@Component({
  selector: 'app-assignment-session-department-info',
  templateUrl: './assignment-session-department-info.component.html',
  styleUrl: './assignment-session-department-info.component.scss',
})
export class AssignmentSessionDepartmentInfoComponent {
  @Output() visibility: EventEmitter<void> = new EventEmitter();
  @Input({ required: true })
  departmentPerfomances: AssignmentSessionDepartmentPerfomance[];
}
