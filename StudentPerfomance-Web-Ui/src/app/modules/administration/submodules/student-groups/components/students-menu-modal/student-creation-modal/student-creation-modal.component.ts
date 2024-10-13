import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ISubbmittable } from '../../../../../../../shared/models/interfaces/isubbmitable';
import { ISuccessNotificatable } from '../../../../../../../shared/models/interfaces/isuccess-notificatable';
import { IFailureNotificatable } from '../../../../../../../shared/models/interfaces/ifailure-notificatable';
import { UserOperationNotificationService } from '../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { StudentGroup } from '../../../services/studentsGroup.interface';
import { StudentCreationService } from './student-creation.service';
import { StudentBaseForm } from './student-base-form';
import { StudentCreationHandler } from './student-creation-handler';
import { catchError, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-student-creation-modal',
  templateUrl: './student-creation-modal.component.html',
  styleUrl: './student-creation-modal.component.scss',
  providers: [UserOperationNotificationService, StudentCreationService],
})
export class StudentCreationModalComponent
  extends StudentBaseForm
  implements
    ISubbmittable,
    ISuccessNotificatable,
    IFailureNotificatable,
    OnInit
{
  @Input({ required: true }) group: StudentGroup;
  @Output() visibility: EventEmitter<boolean> = new EventEmitter();

  protected selectedState: string;

  protected isSuccess: boolean;
  protected isFailure: boolean;

  public constructor(
    private readonly _creationService: StudentCreationService,
    protected readonly notificationService: UserOperationNotificationService
  ) {
    super();
    this.isSuccess = false;
    this.isFailure = false;
    this.selectedState = '';
  }

  public ngOnInit(): void {
    this.selectedState = 'Активен';
    this.initForm();
  }

  public notifyFailure(): void {
    this.isFailure = true;
  }

  protected manageFailure(value: boolean): void {
    this.isFailure = value;
  }

  public notifySuccess(): void {
    this.isSuccess = true;
  }

  protected manageSuccess(value: boolean): void {
    this.isSuccess = value;
  }

  public submit(): void {
    const student = this.createStudentFromForm(this.group);
    student.state = this.selectedState;

    const handler = StudentCreationHandler(
      this.notificationService,
      this,
      this
    );

    this._creationService
      .create(student)
      .pipe(
        tap((response) => handler.handle(response)),
        catchError((error: HttpErrorResponse) => handler.handleError(error))
      )
      .subscribe();
    this.ngOnInit();
  }

  protected close(): void {
    this.visibility.emit(false);
  }

  protected selectState(state: string): void {
    this.selectedState = state;
  }
}
