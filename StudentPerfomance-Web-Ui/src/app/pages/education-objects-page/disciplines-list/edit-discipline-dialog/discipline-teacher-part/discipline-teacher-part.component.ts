import { Component, Input } from '@angular/core';
import {
  EducationPlan,
  EducationPlanSemester,
  SemesterDiscipline,
} from '../../../../../modules/administration/submodules/education-plans/models/education-plan-interface';
import { EducationDirection } from '../../../../../modules/administration/submodules/education-directions/models/education-direction-interface';
import { NgIf } from '@angular/common';
import { RedButtonComponent } from '../../../../../building-blocks/buttons/red-button/red-button.component';
import { DisciplineDepartmentsListComponent } from './discipline-departments-list/discipline-departments-list.component';
import { Department } from '../../../../../modules/administration/submodules/departments/models/departments.interface';
import { DisciplineTeachersListComponent } from './discipline-teachers-list/discipline-teachers-list.component';
import { Teacher } from '../../../../../modules/administration/submodules/teachers/models/teacher.interface';
import { NotificationService } from '../../../../../building-blocks/notifications/notification.service';
import { DisciplineTeacherAttachmentService } from './discipline-teachers-list/discipline-teacher-attachment.service';
import { SemesterPlan } from '../../../../../modules/administration/submodules/semester-plans/models/semester-plan.interface';
import { Semester } from '../../../../../modules/administration/submodules/semesters/models/semester.interface';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { UnauthorizedErrorHandler } from '../../../../../shared/models/common/401-error-handler/401-error-handler.service';
import { animate, style, transition, trigger } from '@angular/animations';

@Component({
  selector: 'app-discipline-teacher-part',
  imports: [
    NgIf,
    RedButtonComponent,
    DisciplineDepartmentsListComponent,
    DisciplineTeachersListComponent,
  ],
  templateUrl: './discipline-teacher-part.component.html',
  styleUrl: './discipline-teacher-part.component.scss',
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
export class DisciplineTeacherPartComponent {
  @Input({ required: true }) discipline: SemesterDiscipline;
  @Input({ required: true }) semester: EducationPlanSemester;
  @Input({ required: true }) educationPlan: EducationPlan;
  @Input({ required: true }) direction: EducationDirection;
  public selectedDepartment: Department | null;
  public teachersOfSelectedDepartment: Teacher[] | null = null;

  public handleDepartmentSelected(department: Department): void {
    this.selectedDepartment = department;
    this.teachersOfSelectedDepartment = department.teachers;
  }

  public constructor(
    private readonly _notifications: NotificationService,
    private readonly _service: DisciplineTeacherAttachmentService,
    private readonly _handler: UnauthorizedErrorHandler,
  ) {}

  public handleSelectedTeacher(teacher: Teacher): void {
    if (!this.selectedDepartment) {
      this._notifications.bulkFailure('Преподаватель не выбран');
      return;
    }
    const disciplinePayload: SemesterPlan = {} as SemesterPlan;
    disciplinePayload.discipline = this.discipline.disciplineName;
    const semesterPayload: Semester = {} as Semester;
    semesterPayload.number = this.semester.number;
    this._service
      .attachTeacher(
        disciplinePayload,
        semesterPayload,
        this.educationPlan,
        this.direction,
        teacher,
        this.selectedDepartment,
      )
      .pipe(
        tap(() => {
          this._notifications.bulkSuccess(
            'Закреплён преподаватель у дисциплины',
          );
          this.selectedDepartment = null;
          this.discipline.teacher = teacher;
        }),
        catchError((error: HttpErrorResponse) => {
          this._handler.tryHandle(error);
          this._notifications.bulkFailure(error.error);
          return new Observable<never>();
        }),
      )
      .subscribe();
  }

  public handleTeacherDetachment(): void {
    if (!this.discipline.teacher) {
      this._notifications.bulkFailure('Преподаватель и так не закреплён');
      return;
    }
    const disciplinePayload: SemesterPlan = {} as SemesterPlan;
    disciplinePayload.discipline = this.discipline.disciplineName;
    disciplinePayload.teacher = this.discipline.teacher;
    const semesterPayload: Semester = {} as Semester;
    semesterPayload.number = this.semester.number;
    this._service
      .detachTeacher(
        disciplinePayload,
        semesterPayload,
        this.educationPlan,
        this.direction,
      )
      .pipe(
        tap(() => {
          this._notifications.bulkSuccess(
            'Преподаватель откреплён от дисциплины',
          );
          this.discipline.teacher = null;
        }),
        catchError((error: HttpErrorResponse) => {
          this._handler.tryHandle(error);
          this._notifications.bulkFailure(error.error);
          return new Observable<never>();
        }),
      )
      .subscribe();
  }
}
