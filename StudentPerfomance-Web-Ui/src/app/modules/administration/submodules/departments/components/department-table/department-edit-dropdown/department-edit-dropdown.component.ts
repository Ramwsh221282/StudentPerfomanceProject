import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ISubbmittable } from '../../../../../../../shared/models/interfaces/isubbmitable';
import { Department } from '../../../models/departments.interface';
import { UserOperationNotificationService } from '../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { DepartmentEditService } from '../../../services/department-edit.service';
import { IsNullOrWhiteSpace } from '../../../../../../../shared/utils/string-helper';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-department-edit-dropdown',
  templateUrl: './department-edit-dropdown.component.html',
  styleUrl: './department-edit-dropdown.component.scss',
})
export class DepartmentEditDropdownComponent implements ISubbmittable, OnInit {
  @Input({ required: true }) department: Department;
  @Input({ required: true }) visibility: boolean = false;
  @Output() visibilityChange: EventEmitter<boolean> = new EventEmitter();

  private departmentCopy: Department;

  public constructor(
    private readonly _notificationService: UserOperationNotificationService,
    private readonly _editService: DepartmentEditService,
  ) {}

  public ngOnInit(): void {
    this.departmentCopy = { ...this.department };
  }

  public submit(): void {
    if (IsNullOrWhiteSpace(this.department.name)) {
      this._notificationService.SetMessage = 'Название кафедры было пустым';
      this._notificationService.failure();
      return;
    }
    const message = this.buildNotificationMessage();
    this.close();
    this._editService
      .update(this.departmentCopy, this.department)
      .pipe(
        tap((response) => {
          this._notificationService.SetMessage = message;
          this._notificationService.success();
          this.close();
        }),
        catchError((error: HttpErrorResponse) => {
          this._notificationService.SetMessage = error.error;
          this._notificationService.failure();
          this.department.name = this.departmentCopy.name;
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

  private buildNotificationMessage(): string {
    return `Кафедра переименована из ${this.departmentCopy.name} в ${this.department.name}`;
  }
}
