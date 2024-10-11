import { Component, EventEmitter, Input, Output } from '@angular/core';
import { SemesterDisciplinesRemoveService } from '../semester-disciplines-remove.service';
import { Semester } from '../../../../../../semesters/models/semester.interface';
import { UserOperationNotificationService } from '../../../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { IFailureNotificatable } from '../../../../../../../../../shared/models/interfaces/ifailure-notificatable';
import { SemesterPlan } from '../../../../../../semester-plans/models/semester-plan.interface';
import { catchError, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { ISuccessNotificatable } from '../../../../../../../../../shared/models/interfaces/isuccess-notificatable';
import { SemesterDisciplineDeletionHandler } from './semester-discipline-deletion-handler';

@Component({
  selector: 'app-semester-disciplines-deletion-modal',
  templateUrl: './semester-disciplines-deletion-modal.component.html',
  styleUrl: './semester-disciplines-deletion-modal.component.scss',
  providers: [
    SemesterDisciplinesRemoveService,
    UserOperationNotificationService,
  ],
})
export class SemesterDisciplinesDeletionModalComponent
  implements IFailureNotificatable, ISuccessNotificatable
{
  @Input({ required: true }) semesterPlan: SemesterPlan;
  @Input({ required: true }) semester: Semester;
  @Output() visibility: EventEmitter<boolean> = new EventEmitter<boolean>();
  @Output() refreshTable: EventEmitter<any> = new EventEmitter<any>();

  protected isFailure: boolean;
  protected isSuccess: boolean;

  public constructor(
    private readonly _removeService: SemesterDisciplinesRemoveService,
    protected readonly notificationService: UserOperationNotificationService
  ) {
    this.isFailure = false;
    this.isSuccess = false;
  }

  public notifySuccess(): void {
    this.isSuccess = true;
  }

  public manageSuccess(value: boolean): void {
    this.isSuccess = value;
    this.refreshTable.emit();
    this.visibility.emit(value);
  }

  public notifyFailure(): void {
    this.isFailure = true;
  }

  public manageFailure(value: boolean): void {
    this.isFailure = value;
    this.visibility.emit(value);
  }

  protected confirm(): void {
    const handler = SemesterDisciplineDeletionHandler(
      this.notificationService,
      this,
      this
    );
    this._removeService
      .remove(this.semester, this.semesterPlan)
      .pipe(
        tap((response) => handler.handle(response)),
        catchError((error: HttpErrorResponse) => handler.handleError(error))
      )
      .subscribe();
    this.refreshTable.emit();
  }

  protected decline(): void {
    this.visibility.emit(false);
  }
}
