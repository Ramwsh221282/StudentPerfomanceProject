import { Component, EventEmitter, Input, Output } from '@angular/core';
import { RedOutlineButtonComponent } from '../../../../building-blocks/buttons/red-outline-button/red-outline-button.component';
import { Student } from '../../../../modules/administration/submodules/students/models/student.interface';
import { StudentGroup } from '../../../../modules/administration/submodules/student-groups/services/studentsGroup.interface';
import { NgClass, NgForOf, NgIf } from '@angular/common';
import { ChangeStudentDataDialogComponent } from './change-student-data-dialog/change-student-data-dialog.component';
import { MoveStudentDialogComponent } from './move-student-dialog/move-student-dialog.component';
import { animate, style, transition, trigger } from '@angular/animations';

@Component({
  selector: 'app-edit-student-dialog',
  imports: [
    RedOutlineButtonComponent,
    NgForOf,
    NgClass,
    ChangeStudentDataDialogComponent,
    NgIf,
    MoveStudentDialogComponent,
  ],
  templateUrl: './edit-student-dialog.component.html',
  styleUrl: './edit-student-dialog.component.scss',
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
export class EditStudentDialogComponent {
  @Input({ required: true }) student: Student;
  @Input({ required: true }) studentGroup: StudentGroup;
  @Output() visibilityChanged: EventEmitter<void> = new EventEmitter();

  public sections: any = [
    { id: 0, name: 'Редактирование данных' },
    { id: 1, name: 'Изменение группы' },
  ];

  public sectionId = 0;

  public selectSection(section: any, $event: MouseEvent): void {
    $event.stopPropagation();
    this.sectionId = section.id;
  }
}
