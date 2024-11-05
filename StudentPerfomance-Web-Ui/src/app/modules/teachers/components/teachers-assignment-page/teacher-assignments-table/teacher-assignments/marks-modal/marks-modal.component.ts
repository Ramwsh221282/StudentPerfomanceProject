import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { TeacherJournalStudent } from '../../../../../models/teacher-journal-students';
import { TeacherJournalDiscipline } from '../../../../../models/teacher-journal-disciplines';
import { ISubbmittable } from '../../../../../../../shared/models/interfaces/isubbmitable';

@Component({
  selector: 'app-marks-modal',
  templateUrl: './marks-modal.component.html',
  styleUrl: './marks-modal.component.scss',
})
export class MarksModalComponent implements ISubbmittable {
  @Input({ required: true }) student: TeacherJournalStudent;
  @Input({ required: true }) discipline: TeacherJournalDiscipline;
  @Output() visibility: EventEmitter<void> = new EventEmitter();
  @Output() markSelected: EventEmitter<TeacherJournalStudent> =
    new EventEmitter();

  protected marks: Mark[];
  protected selectedMark: Mark;

  public constructor() {
    this.marks = [
      { value: 0, name: 'Нет аттестации' } as Mark,
      { value: 1, name: 'Нет проставления' } as Mark,
      { value: 2, name: '2' } as Mark,
      { value: 3, name: '3' } as Mark,
      { value: 4, name: '4' } as Mark,
      { value: 5, name: '5' } as Mark,
    ];
    this.selectedMark = {} as Mark;
  }

  public submit(): void {
    this.student.assignment.value = this.selectedMark.value;
    this.markSelected.emit(this.student);
  }

  protected selectMark(value: any): void {
    const markValue = value.target.value;
    const selectedMark = this.marks.find((m) => m.value == markValue);
    this.selectedMark = selectedMark!;
  }
}

interface Mark {
  value: number;
  name: string;
}
