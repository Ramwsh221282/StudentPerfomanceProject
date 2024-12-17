import { Component, EventEmitter, Input, Output } from '@angular/core';
import { SemesterDiscipline } from '../../../../../models/education-plan-interface';

@Component({
  selector: 'app-discipline-item',
  templateUrl: './discipline-item.component.html',
  styleUrl: './discipline-item.component.scss',
})
export class DisciplineItemComponent {
  @Input({ required: true }) discipline: SemesterDiscipline;
  @Output() teacherAttachmentRequested: EventEmitter<SemesterDiscipline> =
    new EventEmitter();
  @Output() teacherDeattachmentRequested: EventEmitter<SemesterDiscipline> =
    new EventEmitter();
  @Output() disciplineNameUpdateRequested: EventEmitter<SemesterDiscipline> =
    new EventEmitter();
  @Output() disciplineDeletionRequested: EventEmitter<SemesterDiscipline> =
    new EventEmitter();

  protected buildTeacherInfo(): string {
    if (this.discipline.teacher == null) return 'Преподаватель не задан';
    return `${this.discipline.teacher.surname} ${this.discipline.teacher.name[0]}. ${this.discipline.teacher.patronymic == null ? '' : this.discipline.teacher.patronymic[0]}.`;
  }
}
