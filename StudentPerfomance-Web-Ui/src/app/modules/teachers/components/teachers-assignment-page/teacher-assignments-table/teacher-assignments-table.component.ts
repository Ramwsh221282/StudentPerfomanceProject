import { Component, OnInit } from '@angular/core';
import { TeacherAssignmentsDataService } from './teacher-assignments-data.service';
import { TeacherAssignmentInfo } from '../../../models/teacher-assignment-info';
import { TeacherJournal } from '../../../models/teacher-journal';
import { TeacherJournalDiscipline } from '../../../models/teacher-journal-disciplines';

@Component({
  selector: 'app-teacher-assignments-table',
  templateUrl: './teacher-assignments-table.component.html',
  styleUrl: './teacher-assignments-table.component.scss',
  providers: [TeacherAssignmentsDataService],
})
export class TeacherAssignmentsTableComponent implements OnInit {
  protected _teacherAssignments: TeacherAssignmentInfo;
  protected selectedTeacherJournal: TeacherJournal | null = null;
  protected selectedTeacherDiscipline: TeacherJournalDiscipline | null = null;
  protected activeDisciplineName: string = '';

  protected readonly marks: string[] = [
    'Нет аттестации',
    'Нет проставления',
    '2',
    '3',
    '4',
    '5',
  ];

  // protected _teacherJournal: TeacherJournal;
  // protected _teacherDiscipline: TeacherJournalDiscipline;
  // protected _teacherDisciplineCopy: TeacherJournalDiscipline;

  // @Output() selectedDisciplineEmitter: EventEmitter<string> =
  //   new EventEmitter();
  // @Output() selectedGroupEmitter: EventEmitter<string> = new EventEmitter();

  public constructor(private readonly _service: TeacherAssignmentsDataService) {
    this._teacherAssignments = {} as TeacherAssignmentInfo;
  }

  public ngOnInit(): void {
    this._service.getTeacherAssignmentsInfo().subscribe((response) => {
      this._teacherAssignments = response;
    });
  }

  protected handleSelectedJournal(journal: TeacherJournal): void {
    this.selectedTeacherJournal = journal;
    this.selectedTeacherDiscipline = journal.disciplines[0];
    this.activeDisciplineName = this.selectedTeacherDiscipline.name.name;
  }

  // protected selectGroup(journal: TeacherJournal): void {
  //   this._teacherJournal = journal;
  //   this.selectedGroupEmitter.emit(this._teacherJournal.groupName.name);
  //   this._teacherDiscipline = {} as TeacherJournalDiscipline;
  // }
  //
  // protected selectDiscipline(discipline: TeacherJournalDiscipline) {
  //   this._teacherDiscipline = discipline;
  //   this._teacherDisciplineCopy = { ...discipline };
  //   this.selectedDisciplineEmitter.emit(this._teacherDiscipline.name.name);
  // }
  //
  // protected filter(input: any) {
  //   if (this._teacherDiscipline.students == undefined) {
  //     return;
  //   }
  //   const value = input.target.value;
  //   if (value == undefined || value == '') {
  //     this._teacherDiscipline.students = this._teacherDisciplineCopy.students;
  //   }
  //   this._teacherDiscipline.students = this._teacherDiscipline.students.filter(
  //     (s) =>
  //       s.name.startsWith(value) ||
  //       s.surname.startsWith(value) ||
  //       s.patronymic?.startsWith(value),
  //   );
  // }
}
