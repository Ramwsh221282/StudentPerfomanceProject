import { Component, OnInit } from '@angular/core';
import { TeacherAssignmentsDataService } from './teacher-assignments-data.service';
import { TeacherAssignmentInfo } from '../../../models/teacher-assignment-info';
import { TeacherJournal } from '../../../models/teacher-journal';
import { TeacherJournalDiscipline } from '../../../models/teacher-journal-disciplines';
import { catchError, Observable, tap } from 'rxjs';
import { AuthService } from '../../../../../pages/user-page/services/auth.service';
import { AdminAccessResponse } from '../admin-assignments-access-resolver-dialog/admin-assignments-access.service';

@Component({
    selector: 'app-teacher-assignments-table',
    templateUrl: './teacher-assignments-table.component.html',
    styleUrl: './teacher-assignments-table.component.scss',
    providers: [TeacherAssignmentsDataService],
    standalone: false
})
export class TeacherAssignmentsTableComponent implements OnInit {
  protected _teacherAssignments: TeacherAssignmentInfo;
  protected selectedTeacherJournal: TeacherJournal | null = null;
  protected selectedTeacherDiscipline: TeacherJournalDiscipline | null = null;
  protected activeDisciplineName: string = '';
  protected isAdminAccess: boolean = false;
  protected adminAccess: AdminAccessResponse | null = null;

  public constructor(
    private readonly _service: TeacherAssignmentsDataService,
    private readonly _authService: AuthService,
  ) {
    this._teacherAssignments = {} as TeacherAssignmentInfo;
  }

  public ngOnInit(): void {
    this._service
      .getTeacherAssignmentsInfo()
      .pipe(
        tap((response) => {
          this._teacherAssignments = response;
        }),
        catchError(() => {
          if (this._authService.userData.role == 'Администратор')
            this.isAdminAccess = true;
          return new Observable();
        }),
      )
      .subscribe();
  }

  protected handleSelectedJournal(journal: TeacherJournal): void {
    this.selectedTeacherJournal = journal;
    this.selectedTeacherDiscipline = journal.disciplines[0];
    this.activeDisciplineName = this.selectedTeacherDiscipline.name.name;
  }

  protected handleAdminAccessReceived(adminAccess: AdminAccessResponse): void {
    this._service
      .getTeacherAssignmentsInfo(adminAccess)
      .pipe(
        tap((response) => {
          this._teacherAssignments = response;
          this.adminAccess = adminAccess;
        }),
        catchError(() => {
          return new Observable<never>();
        }),
      )
      .subscribe();
  }
}
