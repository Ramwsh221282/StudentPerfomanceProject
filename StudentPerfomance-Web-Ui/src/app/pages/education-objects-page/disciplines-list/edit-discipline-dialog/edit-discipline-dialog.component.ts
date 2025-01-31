import { Component, EventEmitter, Input, Output } from '@angular/core';
import {
  EducationPlan,
  EducationPlanSemester,
  SemesterDiscipline,
} from '../../../../modules/administration/submodules/education-plans/models/education-plan-interface';
import { EducationDirection } from '../../../../modules/administration/submodules/education-directions/models/education-direction-interface';
import { RedOutlineButtonComponent } from '../../../../building-blocks/buttons/red-outline-button/red-outline-button.component';
import { NgClass, NgForOf, NgIf } from '@angular/common';
import { ChangeDisciplineNamePartComponent } from './change-discipline-name-part/change-discipline-name-part.component';
import { DisciplineTeacherPartComponent } from './discipline-teacher-part/discipline-teacher-part.component';
import { animate, style, transition, trigger } from '@angular/animations';

@Component({
  selector: 'app-edit-discipline-dialog',
  imports: [
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
  }
}
