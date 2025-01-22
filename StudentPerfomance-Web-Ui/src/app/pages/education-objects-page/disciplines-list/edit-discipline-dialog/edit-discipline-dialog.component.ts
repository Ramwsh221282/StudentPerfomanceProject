import { Component, EventEmitter, Input, Output } from '@angular/core';
import {
  EducationPlan,
  EducationPlanSemester,
  SemesterDiscipline,
} from '../../../../modules/administration/submodules/education-plans/models/education-plan-interface';
import { EducationDirection } from '../../../../modules/administration/submodules/education-directions/models/education-direction-interface';
import { RedButtonComponent } from '../../../../building-blocks/buttons/red-button/red-button.component';
import { RedOutlineButtonComponent } from '../../../../building-blocks/buttons/red-outline-button/red-outline-button.component';
import { NgClass, NgForOf, NgIf } from '@angular/common';
import { ChangeDisciplineNamePartComponent } from './change-discipline-name-part/change-discipline-name-part.component';
import { DisciplineTeacherPartComponent } from './discipline-teacher-part/discipline-teacher-part.component';

@Component({
  selector: 'app-edit-discipline-dialog',
  imports: [
    RedButtonComponent,
    RedOutlineButtonComponent,
    NgClass,
    NgForOf,
    ChangeDisciplineNamePartComponent,
    DisciplineTeacherPartComponent,
    NgIf,
  ],
  templateUrl: './edit-discipline-dialog.component.html',
  styleUrl: './edit-discipline-dialog.component.scss',
  standalone: true,
})
export class EditDisciplineDialogComponent {
  @Input({ required: true }) discipline: SemesterDiscipline;
  @Input({ required: true }) semester: EducationPlanSemester;
  @Input({ required: true }) educationPlan: EducationPlan;
  @Input({ required: true }) direction: EducationDirection;
  @Output() visibilityChange: EventEmitter<void> = new EventEmitter();

  public sections: any = [
    { id: 0, name: 'Изменение названия' },
    { id: 1, name: 'Преподаватель' },
  ];

  public sectionId = 0;

  public selectSection(section: any): void {
    this.sectionId = section.id;
    console.log(this.sectionId);
  }

  public constructor() {}
}
