import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ISubbmittable } from '../../../../../../shared/models/interfaces/isubbmitable';
import { BaseStudentGroupForm } from '../../models/base-student-group-form';
import { StudentGroupsFacadeService } from '../../services/student-groups-facade.service';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { StudentGroupCreationHandler } from './student-group-creation-handler';
import { catchError, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-student-group-create-modal',
  templateUrl: './student-group-create-modal.component.html',
  styleUrl: './student-group-create-modal.component.scss',
})
export class StudentGroupCreateModalComponent
  extends BaseStudentGroupForm
  implements OnInit, ISubbmittable
{
  @Output() visibility: EventEmitter<boolean> = new EventEmitter<boolean>();
  @Output() refreshRequested: EventEmitter<void> = new EventEmitter();
  @Output() successEmitter: EventEmitter<void> = new EventEmitter();
  @Output() failureEmitter: EventEmitter<void> = new EventEmitter();

  public constructor(
    private readonly _facadeService: StudentGroupsFacadeService,
    protected readonly notificationService: UserOperationNotificationService,
  ) {
    super();
  }

  public submit(): void {
    const group = this.createGroupFromForm();
    const handler = StudentGroupCreationHandler(
      this._facadeService,
      this.notificationService,
      this.refreshRequested,
      this.successEmitter,
      this.failureEmitter,
    );

    this._facadeService
      .create(group)
      .pipe(
        tap((response) => {
          handler.handle(response);
          this.close();
        }),
        catchError((error: HttpErrorResponse) => {
          this.close();
          return handler.handleError(error);
        }),
      )
      .subscribe();
  }

  public ngOnInit(): void {
    this.initForm();
  }

  protected close(): void {
    this.visibility.emit(false);
  }
}
