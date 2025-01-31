import { Component, EventEmitter, Input, Output } from '@angular/core';
import {
  EducationPlan,
  EducationPlanSemester,
} from '../../../modules/administration/submodules/education-plans/models/education-plan-interface';
import { SemesterItemComponent } from './semester-item/semester-item.component';
import { NgForOf } from '@angular/common';
import { animate, style, transition, trigger } from '@angular/animations';

@Component({
  selector: 'app-education-plan-semester-list',
  imports: [SemesterItemComponent, NgForOf],
  templateUrl: './education-plan-semester-list.component.html',
  styleUrl: './education-plan-semester-list.component.scss',
  standalone: true,
  animations: [
    trigger('fadeIn', [
      transition(':enter', [
        style({ opacity: 0, transform: 'translateY(-10px)' }),
        animate(
          '300ms ease-out',
          style({ opacity: 1, transform: 'translateY(0)' }),
        ),
      ]),
      transition(':leave', [
        animate(
          '300ms ease-in',
          style({ opacity: 0, transform: 'translateY(-10px)' }),
        ),
      ]),
    ]),
  ],
})
export class EducationPlanSemesterListComponent {
  @Input({ required: true }) educationPlan: EducationPlan;
  public selectedSemester: EducationPlanSemester | null;
  @Output() semesterSelected: EventEmitter<EducationPlanSemester> =
    new EventEmitter();
}
