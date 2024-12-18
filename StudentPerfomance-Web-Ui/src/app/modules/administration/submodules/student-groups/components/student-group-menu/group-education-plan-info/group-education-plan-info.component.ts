import { Component, EventEmitter, Input, Output } from '@angular/core';
import { StudentGroup } from '../../../services/studentsGroup.interface';

@Component({
  selector: 'app-group-education-plan-info',
  templateUrl: './group-education-plan-info.component.html',
  styleUrl: './group-education-plan-info.component.scss',
})
export class GroupEducationPlanInfoComponent {
  @Input({ required: true }) group: StudentGroup;
  @Output() educationPlanChangeRequest: EventEmitter<StudentGroup> =
    new EventEmitter();
  protected planAttachment: string = 'Выберите учебный план';
  protected readonly String = String;
}
