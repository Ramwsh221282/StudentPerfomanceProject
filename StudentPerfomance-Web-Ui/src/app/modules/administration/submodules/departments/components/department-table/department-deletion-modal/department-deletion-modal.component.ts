import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Department } from '../../../models/departments.interface';
import { UserOperationNotificationService } from '../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { DepartmentDeletionService } from './department-deletion.service';
import { ISubbmittable } from '../../../../../../../shared/models/interfaces/isubbmitable';
import { DepartmentDeletionHandler } from './department-deletion-handler';
import { catchError, tap } from 'rxjs';

@Component({
  selector: 'app-department-deletion-modal',
  templateUrl: './department-deletion-modal.component.html',
  styleUrl: './department-deletion-modal.component.scss',
  providers: [DepartmentDeletionService],
})
export class DepartmentDeletionModalComponent implements ISubbmittable {
  @Input({ required: true }) department: Department;
  @Output() visibility: EventEmitter<boolean> = new EventEmitter<boolean>();
  @Output() successEmitter: EventEmitter<void> = new EventEmitter<void>();
  @Output() failureEmitter: EventEmitter<void> = new EventEmitter<void>();

  public constructor(
    protected notificationService: UserOperationNotificationService,
    private readonly _deletionService: DepartmentDeletionService
  ) {}

  public submit(): void {
    const handler = DepartmentDeletionHandler(
      this.notificationService,
      this.successEmitter,
      this.failureEmitter
    );

    this._deletionService
      .remove(this.department)
      .pipe(
        tap((response) => handler.handle(response)),
        catchError((error) => handler.handleError(error))
      )
      .subscribe();
  }
}
