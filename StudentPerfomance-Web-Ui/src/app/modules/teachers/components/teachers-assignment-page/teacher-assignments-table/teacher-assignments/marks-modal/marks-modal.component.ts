import { Component, EventEmitter, Input, Output } from '@angular/core';
import { TeacherJournalStudent } from '../../../../../models/teacher-journal-students';
import { TeacherJournalDiscipline } from '../../../../../models/teacher-journal-disciplines';

@Component({
  selector: 'app-marks-modal',
  templateUrl: './marks-modal.component.html',
  styleUrl: './marks-modal.component.scss',
})
export class MarksModalComponent {
  @Input({ required: true }) student: TeacherJournalStudent;
  @Input({ required: true }) discipline: TeacherJournalDiscipline;
  @Output() visibility: EventEmitter<void> = new EventEmitter();
  @Output() markSelected: EventEmitter<TeacherJournalStudent> =
    new EventEmitter();

  protected marks: Mark[];
  protected selectedMark: Mark;
  protected selectMarkLabel: string = 'Выбрать оценку';
  protected isSelectingMark: boolean = false;

  protected stringMarks: string[] = [
    'Нет аттестации',
    'Нет проставления',
    '2',
    '3',
    '4',
    '5',
  ];

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

  protected handleMarkSelection(stringMark: string): void {
    this.selectedMark = this.marks.find((m) => m.name == stringMark)!;
    this.student.assignment.value = this.selectedMark.value;
    this.markSelected.emit(this.student);
    this.visibility.emit();
  }
}

interface Mark {
  value: number;
  name: string;
}
