import { Component, EventEmitter, Input, Output } from '@angular/core';
import { StudentGroup } from '../../../services/studentsGroup.interface';

@Component({
    selector: 'app-group-education-plan-info',
    templateUrl: './group-education-plan-info.component.html',
    styleUrl: './group-education-plan-info.component.scss',
    standalone: false
})
export class GroupEducationPlanInfoComponent {
  @Input({ required: true }) group: StudentGroup;
  @Output() educationPlanChangeRequest: EventEmitter<StudentGroup> =
    new EventEmitter();
  @Output() educationPlanDeattachmentRequest: EventEmitter<StudentGroup> =
    new EventEmitter();
  protected readonly String = String;
}
