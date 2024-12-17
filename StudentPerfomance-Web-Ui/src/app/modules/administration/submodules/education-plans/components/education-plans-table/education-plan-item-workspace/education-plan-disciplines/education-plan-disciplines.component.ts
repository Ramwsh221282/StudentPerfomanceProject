import {
  ChangeDetectionStrategy,
  Component,
  EventEmitter,
  Input,
  Output,
} from '@angular/core';
import {
  EducationPlan,
  EducationPlanSemester,
  SemesterDiscipline,
} from '../../../../models/education-plan-interface';
import { ISubbmittable } from '../../../../../../../../shared/models/interfaces/isubbmitable';
import { SemesterDisciplinesCreationService } from '../semester-disciplines-create.service';
import { UserOperationNotificationService } from '../../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { SemesterPlan } from '../../../../../semester-plans/models/semester-plan.interface';
import { Semester } from '../../../../../semesters/models/semester.interface';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { SemesterPlanCreationHandler } from './semester-discipline-creation-handler';
import { Teacher } from '../../../../../teachers/models/teacher.interface';
import { SemesterDisciplinesEditService } from '../semester-disciplines-edit.service';
import { SemesterDisciplinesRemoveService } from '../semester-disciplines-remove.service';

@Component({
  selector: 'app-education-plan-disciplines',
  templateUrl: './education-plan-disciplines.component.html',
  styleUrl: './education-plan-disciplines.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class EducationPlanDisciplinesComponent implements ISubbmittable {
  @Input({ required: true }) plan: EducationPlan;
  @Input({ required: true }) semester: EducationPlanSemester;
  @Input({ required: true }) disciplines: SemesterDiscipline[];
  @Output() disciplineAdded: EventEmitter<SemesterDiscipline> =
    new EventEmitter();
  @Output() disciplineTeacherAttached: EventEmitter<SemesterDiscipline> =
    new EventEmitter();
  @Output() disciplineTeacherDeattached: EventEmitter<SemesterDiscipline> =
    new EventEmitter();
  @Output() disciplineNameUpdated: EventEmitter<SemesterDiscipline[]> =
    new EventEmitter();
  @Output() disciplineDeleted: EventEmitter<SemesterDiscipline> =
    new EventEmitter();

  protected disciplineForAttachmentRequest: SemesterDiscipline | null = null;
  protected disciplineForDeattachmentRequest: SemesterDiscipline | null = null;
  protected disciplineForNameUpdateRequest: SemesterDiscipline | null = null;
  protected disciplineForDeletionRequest: SemesterDiscipline | null = null;

  protected disciplineName: string = '';

  public constructor(
    private readonly _createService: SemesterDisciplinesCreationService,
    private readonly _notificationService: UserOperationNotificationService,
    private readonly _disciplinesEditService: SemesterDisciplinesEditService,
    private readonly _disciplineDeletionService: SemesterDisciplinesRemoveService,
  ) {}

  public submit(): void {
    if (this.isDisciplineNameEmpty()) return;
    const semesterPlan: SemesterPlan = this.createSemesterDiscipline();
    const handler = SemesterPlanCreationHandler(this._notificationService);
    this._createService
      .create(semesterPlan)
      .pipe(
        tap((response) => {
          handler.handle(response);
          const addedDiscipline = {} as SemesterDiscipline;
          addedDiscipline.disciplineName = this.disciplineName;
          this.disciplineAdded.emit(addedDiscipline);
        }),
        catchError((error: HttpErrorResponse) => {
          return handler.handleError(error);
        }),
      )
      .subscribe();
  }

  protected handleTeacherAttachment(teacher: Teacher): void {
    if (this.isDisciplineForAttachmentEmpty()) return;
    const plan: SemesterPlan = {} as SemesterPlan;
    const semester: Semester = {} as Semester;
    plan.discipline = this.disciplineForAttachmentRequest!.disciplineName;
    semester.number = this.semester.number;
    semester.educationPlan = { ...this.plan };
    plan.semester = { ...semester };
    plan.teacher = { ...teacher };
    this._disciplinesEditService
      .attachTeacher(plan)
      .pipe(
        tap((response) => {
          const updatedDiscipline = this.disciplines.find(
            (discipline) => discipline.disciplineName == response.discipline,
          );
          if (updatedDiscipline) {
            updatedDiscipline.teacher = { ...response.teacher! };
            this.disciplineTeacherAttached.emit(updatedDiscipline);
          }
        }),
        catchError((error: HttpErrorResponse) => {
          this._notificationService.SetMessage = error.error;
          this._notificationService.failure();
          return new Observable();
        }),
      )
      .subscribe();
  }

  protected handleDisciplineNameUpdate(
    disciplines: SemesterDiscipline[],
  ): void {
    const semester = {} as Semester;
    semester.number = this.semester.number;
    semester.educationPlan = { ...this.plan };

    const initialSemesterPlan = {} as SemesterPlan;
    initialSemesterPlan.discipline = disciplines[1].disciplineName;
    initialSemesterPlan.semester = { ...semester };

    const updatedSemesterPlan = {} as SemesterPlan;
    updatedSemesterPlan.discipline = disciplines[0].disciplineName;
    updatedSemesterPlan.semester = { ...semester };

    this._disciplinesEditService
      .changeName(updatedSemesterPlan, initialSemesterPlan)
      .pipe(
        tap((response) => {
          this.disciplineNameUpdated.emit(disciplines);
        }),
        catchError((error: HttpErrorResponse) => {
          this._notificationService.SetMessage = error.error;
          this._notificationService.failure();
          return new Observable<never>();
        }),
      )
      .subscribe();
  }

  protected handleDisciplineDeletion(discipline: SemesterDiscipline): void {
    const semester = {} as Semester;
    semester.number = this.semester.number;
    semester.educationPlan = { ...this.plan };

    const plan = {} as SemesterPlan;
    plan.discipline = discipline.disciplineName;
    plan.semester = { ...semester };

    this._disciplineDeletionService
      .remove(plan)
      .pipe(
        tap((response) => {
          this.disciplineDeleted.emit(discipline);
        }),
        catchError((error: HttpErrorResponse) => {
          this._notificationService.SetMessage = error.error;
          this._notificationService.failure();
          return new Observable<never>();
        }),
      )
      .subscribe();
  }

  protected handleTeacherDeattachment(discipline: SemesterDiscipline): void {
    if (this.isDisciplineForDeattachmentEmpty()) return;
    const plan: SemesterPlan = {} as SemesterPlan;
    const semester: Semester = {} as Semester;
    plan.discipline = discipline.disciplineName;
    semester.number = this.semester.number;
    semester.educationPlan = { ...this.plan };
    plan.semester = { ...semester };
    this._disciplinesEditService
      .deattachTeacher(plan)
      .pipe(
        tap((response) => {
          this.disciplineTeacherDeattached.emit(discipline);
        }),
        catchError((error: HttpErrorResponse) => {
          this._notificationService.SetMessage = error.error;
          this._notificationService.failure();
          return new Observable<never>();
        }),
      )
      .subscribe();
  }

  private createSemesterDiscipline(): SemesterPlan {
    const plan: SemesterPlan = {} as SemesterPlan;
    const semester: Semester = {} as Semester;
    plan.discipline = this.disciplineName;
    semester.number = this.semester.number;
    semester.educationPlan = { ...this.plan };
    plan.semester = { ...semester };
    return plan;
  }

  private isDisciplineNameEmpty(): boolean {
    if (
      this.disciplineName.length == 0 ||
      this.disciplineName.trim().length == 0
    ) {
      this._notificationService.SetMessage =
        'Необходимо указать наименование дисциплины';
      this._notificationService.failure();
      return true;
    }
    return false;
  }

  private isDisciplineForAttachmentEmpty(): boolean {
    if (!this.disciplineForAttachmentRequest) {
      this._notificationService.SetMessage = 'Дисциплина не была выбрана';
      this._notificationService.failure();
      return true;
    }
    return false;
  }

  private isDisciplineForDeattachmentEmpty(): boolean {
    if (!this.disciplineForDeattachmentRequest) {
      this._notificationService.SetMessage = 'Дисциплина не выбрана';
      this._notificationService.failure();
      return true;
    }
    return false;
  }
}
