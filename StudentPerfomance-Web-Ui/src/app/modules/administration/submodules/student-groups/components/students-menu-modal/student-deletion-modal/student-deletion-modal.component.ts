import { Component, EventEmitter, Input, Output } from '@angular/core';
import { IFailureNotificatable } from '../../../../../../../shared/models/interfaces/ifailure-notificatable';
import { Student } from '../../../../students/models/student.interface';
import { UserOperationNotificationService } from '../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { StudentDeletionService } from './student-deletion.service';
import { ISubbmittable } from '../../../../../../../shared/models/interfaces/isubbmitable';
import { StudentDeletionHandler } from './student-deletion-handler';
import { catchError, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { ISuccessNotificatable } from '../../../../../../../shared/models/interfaces/isuccess-notificatable';

@Component({
  selector: 'app-student-deletion-modal',
  templateUrl: './student-deletion-modal.component.html',
  styleUrl: './student-deletion-modal.component.scss',
  providers: [UserOperationNotificationService, StudentDeletionService],
})
export class StudentDeletionModalComponent
  implements IFailureNotificatable, ISubbmittable, ISuccessNotificatable
{
  @Input({ required: true }) student: Student;
  @Output() visibility: EventEmitter<boolean> = new EventEmitter();

  protected isFailure: boolean;
  protected isSuccess: boolean;

  public constructor(
    protected readonly notificationService: UserOperationNotificationService,
    private readonly _deletionServiec: StudentDeletionService
  ) {
    this.isFailure = false;
    this.isSuccess = false;
  }

  public submit(): void {
    const handler = StudentDeletionHandler(
      this.notificationService,
      this,
      this
    );
    this._deletionServiec
      .remove(this.student)
      .pipe(
        tap((response) => handler.handle(response)),
        catchError((error: HttpErrorResponse) => handler.handleError(error))
      )
      .subscribe();
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

  protected close(): void {
    this.visibility.emit(false);
  }
}
