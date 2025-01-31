import { Component, EventEmitter, Input, Output } from '@angular/core';
import { StudentGroup } from '../../../modules/administration/submodules/student-groups/services/studentsGroup.interface';
import { NgIf } from '@angular/common';
import { RedOutlineButtonComponent } from '../../../building-blocks/buttons/red-outline-button/red-outline-button.component';
import { GreenOutlineButtonComponent } from '../../../building-blocks/buttons/green-outline-button/green-outline-button.component';

@Component({
  selector: 'app-student-group-plan-info',
  imports: [NgIf, RedOutlineButtonComponent, GreenOutlineButtonComponent],
  templateUrl: './student-group-plan-info.component.html',
  styleUrl: './student-group-plan-info.component.scss',
  standalone: true,
})
export class StudentGroupPlanInfoComponent {
  @Input({ required: true }) group: StudentGroup;
  @Output() detachPlanClicked: EventEmitter<StudentGroup> = new EventEmitter();
  @Output() attachPlanClicked: EventEmitter<StudentGroup> = new EventEmitter();
}
