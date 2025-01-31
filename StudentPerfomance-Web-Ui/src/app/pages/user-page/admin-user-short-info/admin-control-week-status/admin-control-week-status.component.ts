import { Component, OnInit } from '@angular/core';
import { CurrentControlWeekDataService } from '../../../current-control-week-page/current-control-week-data.service';
import { UnauthorizedErrorHandler } from '../../../../shared/models/common/401-error-handler/401-error-handler.service';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { AssignmentSession } from '../../../../modules/administration/submodules/assignment-sessions/models/assignment-session-interface';
import { NgIf } from '@angular/common';
import { ControlWeekStatusItemsListComponent } from './control-week-status-items-list/control-week-status-items-list.component';
import { BlueOutlineButtonComponent } from '../../../../building-blocks/buttons/blue-outline-button/blue-outline-button.component';
import { Router } from '@angular/router';
import { animate, style, transition, trigger } from '@angular/animations';

@Component({
  selector: 'app-admin-control-week-status',
  imports: [
    NgIf,
    ControlWeekStatusItemsListComponent,
    BlueOutlineButtonComponent,
  ],
  templateUrl: './admin-control-week-status.component.html',
  styleUrl: './admin-control-week-status.component.scss',
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
export class AdminControlWeekStatusComponent implements OnInit {
  public statuses: AdminControlWeekGroupStatus[] = [];
  public totalGroupsParticipating: number = 0;
  public totalGroupsFullyCompleted: number = 0;
  public totalNotCompletedGroups: number = 0;
  public isLoading: boolean = true;

  public constructor(
    private readonly _service: CurrentControlWeekDataService,
    private readonly _handler: UnauthorizedErrorHandler,
    private readonly _router: Router,
  ) {}

  public navigateOnGradesPage(): void {
    this._router.navigate(['current-control-week']);
  }

  public ngOnInit() {
    this._service
      .getCurrent()
      .pipe(
        tap((response) => {
          this.initializeStatuses(response);
          this.initializeTotalGroupsParticipating();
          this.initializeTotalCompletedGroups();
          this.isLoading = false;
        }),
        catchError((error: HttpErrorResponse) => {
          this.isLoading = false;
          this._handler.tryHandle(error);
          return new Observable<never>();
        }),
      )
      .subscribe();
  }

  private initializeTotalGroupsParticipating(): void {
    this.totalGroupsParticipating = this.statuses.length;
  }

  private initializeTotalCompletedGroups(): void {
    for (const group of this.statuses) {
      for (const discipline of group.disciplines) {
        for (const assignment of discipline.assignments) {
          if (assignment.studentGrade == 'Не проставлена') {
            group.isCompleted = false;
            break;
          }
        }
        if (!group.isCompleted) break;
      }
      if (group.isCompleted) {
        this.totalGroupsFullyCompleted++;
      } else {
        this.totalNotCompletedGroups++;
        this.initializeGroupRequiredTeachers(group);
      }
    }
  }

  private initializeGroupRequiredTeachers(
    group: AdminControlWeekGroupStatus,
  ): void {
    for (const discipline of group.disciplines) {
      for (const grade of discipline.assignments) {
        if (grade.studentGrade == 'Не проставлена') {
          group.requiredDisciplines.push(discipline);
          break;
        }
      }
    }
  }

  private initializeStatuses(session: AssignmentSession): void {
    for (const week of session.weeks) {
      const group: AdminControlWeekGroupStatus =
        {} as AdminControlWeekGroupStatus;
      group.groupName = week.groupName.name;
      group.disciplines = [];
      group.isCompleted = true;
      group.requiredDisciplines = [];
      for (const discipline of week.disciplines) {
        const disciplineStatus: AdminControlWeekDisciplinesStatus =
          {} as AdminControlWeekDisciplinesStatus;
        disciplineStatus.discipline = discipline.discipline.name;
        disciplineStatus.teacher = {
          teacherName: discipline.teacherName.name,
          teacherSurname: discipline.teacherName.surname,
          teacherPatronymic: discipline.teacherName.patronymic,
        };
        disciplineStatus.assignments = [];
        for (const student of discipline.students) {
          const assignment = {} as ControlWeekDisciplineAssignments;
          assignment.studentName = student.name.name;
          assignment.studentSurname = student.name.surname;
          assignment.studentPatronymic = student.name.patronymic;
          assignment.studentGrade = student.value;
          disciplineStatus.assignments.push(assignment);
        }
        group.disciplines.push(disciplineStatus);
      }
      this.statuses.push(group);
    }
  }
}

export interface AdminControlWeekGroupStatus {
  groupName: string;
  disciplines: AdminControlWeekDisciplinesStatus[];
  isCompleted: boolean;
  requiredDisciplines: AdminControlWeekDisciplinesStatus[];
}

export interface AdminControlWeekDisciplinesStatus {
  discipline: string;
  teacher: ControlWeekDisciplineTeacher;
  assignments: ControlWeekDisciplineAssignments[];
}

export interface ControlWeekDisciplineTeacher {
  teacherName: string;
  teacherSurname: string;
  teacherPatronymic: string | null;
}

export interface ControlWeekDisciplineAssignments {
  studentName: string;
  studentSurname: string;
  studentPatronymic: string | null;
  studentGrade: string;
}
