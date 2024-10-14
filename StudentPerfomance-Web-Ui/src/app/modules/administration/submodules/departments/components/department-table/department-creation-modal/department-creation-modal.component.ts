import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { DepartmentFormBase } from '../../../models/base-department-form';
import { ISubbmittable } from '../../../../../../../shared/models/interfaces/isubbmitable';
import { DepartmentCreationService } from './department-creation.service';
import { UserOperationNotificationService } from '../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { Department } from '../../../models/departments.interface';
import { DepartmentCreationHandler } from './department-creation-handler';
import { catchError, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-department-creation-modal',
  templateUrl: './department-creation-modal.component.html',
  styleUrl: './department-creation-modal.component.scss',
  providers: [DepartmentCreationService],
})
export class DepartmentCreationModalComponent
  extends DepartmentFormBase
  implements ISubbmittable, OnInit
{
  @Output() visibility: EventEmitter<boolean> = new EventEmitter<boolean>();
  @Output() successEmitter: EventEmitter<void> = new EventEmitter();
  @Output() failureEmitter: EventEmitter<void> = new EventEmitter();

  public constructor(
    protected readonly notificationService: UserOperationNotificationService,
    private readonly _creationService: DepartmentCreationService
  ) {
    super();
  }

  public submit(): void {
    const department: Department = this.createDepartmentFromForm();
    const handler = DepartmentCreationHandler(
      this.notificationService,
      this.successEmitter,
      this.failureEmitter
    );

    this._creationService
      .create(department)
      .pipe(
        tap((response) => handler.handle(response)),
        catchError((error: HttpErrorResponse) => handler.handleError(error))
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
