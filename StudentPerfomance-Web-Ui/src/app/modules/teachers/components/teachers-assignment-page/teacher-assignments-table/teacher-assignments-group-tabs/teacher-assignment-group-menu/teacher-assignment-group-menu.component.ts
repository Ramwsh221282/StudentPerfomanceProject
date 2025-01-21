import { Component, EventEmitter, Input, Output } from '@angular/core';
import { TeacherJournal } from '../../../../../models/teacher-journal';
import { NgClass, NgForOf, NgIf } from '@angular/common';
import { TeacherJournalDiscipline } from '../../../../../models/teacher-journal-disciplines';

@Component({
    selector: 'app-teacher-assignment-group-menu',
    imports: [NgClass, NgForOf, NgIf],
    templateUrl: './teacher-assignment-group-menu.component.html',
    styleUrl: './teacher-assignment-group-menu.component.scss'
})
export class TeacherAssignmentGroupMenuComponent {
  @Input({ required: true }) journal: TeacherJournal;
  @Input({ required: true }) activeDiscipline: string = '';
  @Output() disciplineSelected: EventEmitter<TeacherJournalDiscipline> =
    new EventEmitter();

  protected handleSelectedDiscipline(
    discipline: TeacherJournalDiscipline,
  ): void {
    this.disciplineSelected.emit(discipline);
    this.activeDiscipline = discipline.name.name;
  }
}
