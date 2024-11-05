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

  protected _teacherJournal: TeacherJournal;
  protected _teacherDiscipline: TeacherJournalDiscipline;

  public constructor(private readonly _service: TeacherAssignmentsDataService) {
    this._teacherAssignments = {} as TeacherAssignmentInfo;
  }

  public ngOnInit(): void {
    this._service.getTeacherAssignmentsInfo().subscribe((response) => {
      this._teacherAssignments = response;
    });
  }

  protected selectGroup(journal: TeacherJournal): void {
    this._teacherJournal = journal;
  }

  protected selectDiscipline(discipline: TeacherJournalDiscipline) {
    this._teacherDiscipline = discipline;
  }
}
