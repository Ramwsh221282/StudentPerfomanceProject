import { Component, EventEmitter, Input, Output } from '@angular/core';
import { EducationPlanSemester } from '../../../../models/education-plan-interface';

@Component({
    selector: 'app-education-plan-semester-button',
    templateUrl: './education-plan-semester-button.component.html',
    styleUrl: './education-plan-semester-button.component.scss',
    standalone: false
})
export class EducationPlanSemesterButtonComponent {
  @Input({ required: true }) semester: EducationPlanSemester;
  @Input() selectedSemester: EducationPlanSemester | null;
  @Output() semesterSelectionChanged =
    new EventEmitter<EducationPlanSemester>();
}
