import { Component, Input } from '@angular/core';
import { BlueButtonComponent } from '../../../../../building-blocks/buttons/blue-button/blue-button.component';
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

@Component({
  selector: 'app-discipline-teacher-part',
  imports: [
    BlueButtonComponent,
    NgIf,
    RedButtonComponent,
    DisciplineDepartmentsListComponent,
    DisciplineTeachersListComponent,
  ],
  templateUrl: './discipline-teacher-part.component.html',
  styleUrl: './discipline-teacher-part.component.scss',
  standalone: true,
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
  ) {}

  public handleSelectedTeacher(teacher: Teacher): void {
    if (!this.selectedDepartment) {
      this._notifications.failure();
      this._notifications.setMessage('Преподаватель не выбран');
      this._notifications.turn();
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
          this._notifications.setMessage(
            'Закреплён преподаватель у дисциплины',
          );
          this._notifications.success();
          this._notifications.turn();
          this.selectedDepartment = null;
          this.discipline.teacher = teacher;
        }),
        catchError((error: HttpErrorResponse) => {
          this._notifications.setMessage(error.error);
          this._notifications.failure();
          this._notifications.turn();
          return new Observable<never>();
        }),
      )
      .subscribe();
  }

  public handleTeacherDetachment(): void {
    if (!this.discipline.teacher) {
      this._notifications.setMessage('Преподаватель и так не закреплён');
      this._notifications.failure();
      this._notifications.turn();
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
          this._notifications.setMessage(
            'Преподаватель откреплён от дисциплины',
          );
          this._notifications.success();
          this._notifications.turn();
          this.discipline.teacher = null;
        }),
        catchError((error: HttpErrorResponse) => {
          this._notifications.setMessage(error.error);
          this._notifications.failure();
          this._notifications.turn();
          return new Observable<never>();
        }),
      )
      .subscribe();
  }
}
