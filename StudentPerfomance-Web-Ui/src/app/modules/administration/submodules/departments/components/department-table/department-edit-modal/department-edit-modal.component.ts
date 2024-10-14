import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { DepartmentEditService } from './department-edit.service';
import { ISubbmittable } from '../../../../../../../shared/models/interfaces/isubbmitable';
import { Department } from '../../../models/departments.interface';
import { UserOperationNotificationService } from '../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { DepartmentEditHandler } from './department-edit-handler';
import { catchError, tap } from 'rxjs';

@Component({
  selector: 'app-department-edit-modal',
  templateUrl: './department-edit-modal.component.html',
  styleUrl: './department-edit-modal.component.scss',
  providers: [DepartmentEditService],
})
export class DepartmentEditModalComponent implements ISubbmittable, OnInit {
  @Input({ required: true }) initial: Department;
  @Output() visibility: EventEmitter<boolean> = new EventEmitter<boolean>();
  @Output() successEmitter: EventEmitter<void> = new EventEmitter<void>();
  @Output() failureEmitter: EventEmitter<void> = new EventEmitter<void>();

  protected activeDepartment: Department;

  public constructor(
    private readonly _editService: DepartmentEditService,
    protected readonly notificationService: UserOperationNotificationService
  ) {
    this.activeDepartment = {} as Department;
  }

  public ngOnInit(): void {
    this.activeDepartment = { ...this.initial };
  }

  public submit(): void {
    const handler = DepartmentEditHandler(
      this.notificationService,
      this.successEmitter,
      this.failureEmitter,
      this.initial
    );

    this._editService
      .update(this.initial, this.activeDepartment)
      .pipe(
        tap((response) => handler.handle(response)),
        catchError((error) => handler.handleError(error))
      )
      .subscribe();
  }
}
