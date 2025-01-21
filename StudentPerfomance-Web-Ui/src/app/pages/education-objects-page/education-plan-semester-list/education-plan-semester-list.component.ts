import { Component, EventEmitter, Input, Output } from '@angular/core';
import {
  EducationPlan,
  EducationPlanSemester,
} from '../../../modules/administration/submodules/education-plans/models/education-plan-interface';
import { SemesterItemComponent } from './semester-item/semester-item.component';
import { NgForOf } from '@angular/common';

@Component({
  selector: 'app-education-plan-semester-list',
  imports: [SemesterItemComponent, NgForOf],
  templateUrl: './education-plan-semester-list.component.html',
  styleUrl: './education-plan-semester-list.component.scss',
  standalone: true,
})
export class EducationPlanSemesterListComponent {
  @Input({ required: true }) educationPlan: EducationPlan;
  public selectedSemester: EducationPlanSemester | null;
  @Output() semesterSelected: EventEmitter<EducationPlanSemester> =
    new EventEmitter();
}
