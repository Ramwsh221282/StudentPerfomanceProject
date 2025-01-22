import { Component, EventEmitter, Input, Output } from '@angular/core';
import { NgClass, NgOptimizedImage } from '@angular/common';
import { EducationPlanSemester } from '../../../../modules/administration/submodules/education-plans/models/education-plan-interface';

@Component({
  selector: 'app-semester-item',
  imports: [NgOptimizedImage, NgClass],
  templateUrl: './semester-item.component.html',
  styleUrl: './semester-item.component.scss',
  standalone: true,
})
export class SemesterItemComponent {
  @Input({ required: true }) semester: EducationPlanSemester;
  @Input({ required: true }) isSelectedSemester: boolean;
  @Output() selectSemester: EventEmitter<EducationPlanSemester> =
    new EventEmitter();
}
