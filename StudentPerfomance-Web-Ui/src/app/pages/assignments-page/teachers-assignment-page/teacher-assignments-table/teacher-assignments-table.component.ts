import { Component, OnInit } from '@angular/core';
import { catchError, Observable, tap } from 'rxjs';
import { AdminAccessResponse } from '../admin-assignments-access-resolver-dialog/admin-assignments-access.service';
import { TeacherAssignmentsGroupTabsComponent } from './teacher-assignments-group-tabs/teacher-assignments-group-tabs.component';
import { NgForOf, NgIf } from '@angular/common';
import { TeacherAssignmentGroupMenuComponent } from './teacher-assignments-group-tabs/teacher-assignment-group-menu/teacher-assignment-group-menu.component';
import { TeacherAssignmentsComponent } from './teacher-assignments/teacher-assignments.component';
import { AdminAssignmentsAccessResolverDialogComponent } from '../admin-assignments-access-resolver-dialog/admin-assignments-access-resolver-dialog.component';
import { TeacherAssignmentInfo } from '../../models/teacher-assignment-info';
import { TeacherJournal } from '../../models/teacher-journal';
import { TeacherJournalDiscipline } from '../../models/teacher-journal-disciplines';
import { TeacherAssignmentsDataService } from '../../../user-page/teacher-user-short-info/teacher-assignments-data.service';
import { AuthService } from '../../../user-page/services/auth.service';

@Component({
  selector: 'app-teacher-assignments-table',
  templateUrl: './teacher-assignments-table.component.html',
  styleUrl: './teacher-assignments-table.component.scss',
  providers: [TeacherAssignmentsDataService],
  standalone: true,
  imports: [
    TeacherAssignmentsGroupTabsComponent,
    NgIf,
    TeacherAssignmentGroupMenuComponent,
    TeacherAssignmentsComponent,
    AdminAssignmentsAccessResolverDialogComponent,
    NgForOf,
  ],
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
