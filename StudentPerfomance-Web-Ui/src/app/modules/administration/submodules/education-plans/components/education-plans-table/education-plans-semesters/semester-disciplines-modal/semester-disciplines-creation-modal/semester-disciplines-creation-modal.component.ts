import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { SemesterDisciplinesCreationService } from '../semester-disciplines-create.service';
import { Semester } from '../../../../../../semesters/models/semester.interface';
import { SemesterDisciplineBaseForm } from '../models/semester-discipline-base-form';
import { ISubbmittable } from '../../../../../../../../../shared/models/interfaces/isubbmitable';
import { ISuccessNotificatable } from '../../../../../../../../../shared/models/interfaces/isuccess-notificatable';
import { IFailureNotificatable } from '../../../../../../../../../shared/models/interfaces/ifailure-notificatable';
import { SemesterPlanCreationHandler } from './semester-discipline-creation-handler';
import { UserOperationNotificationService } from '../../../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { catchError, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-semester-disciplines-creation-modal',
  templateUrl: './semester-disciplines-creation-modal.component.html',
  styleUrl: './semester-disciplines-creation-modal.component.scss',
  providers: [
    SemesterDisciplinesCreationService,
    UserOperationNotificationService,
  ],
})
export class SemesterDisciplinesCreationModalComponent
  extends SemesterDisciplineBaseForm
  implements
    OnInit,
    ISubbmittable,
    ISuccessNotificatable,
    IFailureNotificatable
{
  @Input({ required: true }) semester: Semester;
  @Output() visibility: EventEmitter<boolean> = new EventEmitter<boolean>();
  @Output() updateDisciplines: EventEmitter<any> = new EventEmitter();

  protected isSuccess: boolean;
  protected isFailure: boolean;

  public constructor(
    private readonly _creationService: SemesterDisciplinesCreationService,
    protected readonly notificationService: UserOperationNotificationService
  ) {
    super();
    this.isSuccess = false;
    this.isFailure = false;
  }

  public notifyFailure(): void {
    this.isFailure = true;
  }

  public manageFailure(value: boolean): void {
    this.isFailure = value;
  }

  public notifySuccess(): void {
    this.isSuccess = true;
  }

  public manageSuccess(value: boolean): void {
    this.isSuccess = value;
  }

  public submit(): void {
    const plan = this.createSemesterPlanFromForm();
    const handler = SemesterPlanCreationHandler(
      this._creationService,
      this.notificationService,
      this,
      this
    );
    this._creationService
      .create(this.semester, plan)
      .pipe(
        tap((response) => {
          handler.handle(response);
          this.updateDisciplines.emit();
        }),
        catchError((error: HttpErrorResponse) => handler.handleError(error))
      )
      .subscribe();
    this.ngOnInit();
  }

  public ngOnInit(): void {
    this.initForm();
  }

  protected close(): void {
    this.visibility.emit(false);
  }
}
