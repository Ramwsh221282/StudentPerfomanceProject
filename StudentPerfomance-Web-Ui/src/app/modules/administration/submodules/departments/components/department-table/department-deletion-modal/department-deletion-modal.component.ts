import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Department } from '../../../models/departments.interface';
import { UserOperationNotificationService } from '../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { DepartmentDeletionService } from '../../../services/department-deletion.service';
import { ISubbmittable } from '../../../../../../../shared/models/interfaces/isubbmitable';
import { catchError, Observable, tap } from 'rxjs';

@Component({
    selector: 'app-department-deletion-modal',
    templateUrl: './department-deletion-modal.component.html',
    styleUrl: './department-deletion-modal.component.scss',
    providers: [DepartmentDeletionService],
    standalone: false
})
export class DepartmentDeletionModalComponent implements ISubbmittable {
  @Input({ required: true }) department: Department;
  @Input({ required: true }) visibility: boolean = false;
  @Output() visibilityChange: EventEmitter<boolean> =
    new EventEmitter<boolean>();
  @Output() deleteCommited: EventEmitter<Department> =
    new EventEmitter<Department>();

  public constructor(
    private readonly _notificationService: UserOperationNotificationService,
    private readonly _deletionService: DepartmentDeletionService,
  ) {}

  public submit(): void {
    this._deletionService
      .remove(this.department)
      .pipe(
        tap((response) => {
          this._notificationService.SetMessage = `Удалена кафедра ${this.department.name}`;
          this._notificationService.success();
          this.deleteCommited.emit(this.department);
          this.close();
        }),
        catchError((error) => {
          this._notificationService.SetMessage = error.error;
          this._notificationService.failure();
          this.close();
          return new Observable<never>();
        }),
      )
      .subscribe();
  }

  protected close(): void {
    this.visibility = false;
    this.visibilityChange.emit(this.visibility);
  }
}
