import { Component, EventEmitter, Input, Output } from '@angular/core';
import { StudentGroup } from '../../services/studentsGroup.interface';
import { StudentGroupsFacadeService } from '../../services/student-groups-facade.service';
import { IFailureNotificatable } from '../../../../../../shared/models/interfaces/ifailure-notificatable';
import { StudentGroupDeletionHandler } from './student-group-deletion-handler';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { catchError, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-student-group-remove-modal',
  templateUrl: './student-group-remove-modal.component.html',
  styleUrl: './student-group-remove-modal.component.scss',
  providers: [UserOperationNotificationService],
})
export class StudentGroupRemoveModalComponent implements IFailureNotificatable {
  @Input({ required: true }) group: StudentGroup;
  @Output() visibility: EventEmitter<boolean> = new EventEmitter();

  protected isFailure: boolean;

  public constructor(
    private readonly _facadeService: StudentGroupsFacadeService,
    protected readonly notificationService: UserOperationNotificationService
  ) {
    this.isFailure = false;
  }

  public notifyFailure(): void {
    this.isFailure = true;
  }

  public manageFailure(value: boolean): void {
    this.isFailure = value;
  }

  public confirm(): void {
    const handler = StudentGroupDeletionHandler(
      this._facadeService,
      this.notificationService,
      this
    );

    this._facadeService
      .delete(this.group)
      .pipe(
        tap((response) => handler.handle(response)),
        catchError((error: HttpErrorResponse) => handler.handleError(error))
      )
      .subscribe();
  }

  public decline(): void {
    this.close();
  }

  public close(): void {
    this.visibility.emit(false);
  }
}
