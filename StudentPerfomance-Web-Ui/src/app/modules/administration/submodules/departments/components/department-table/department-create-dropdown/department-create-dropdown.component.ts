import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ISubbmittable } from '../../../../../../../shared/models/interfaces/isubbmitable';
import { Department } from '../../../models/departments.interface';
import { IsNullOrWhiteSpace } from '../../../../../../../shared/utils/string-helper';
import { DepartmentCreationService } from '../../../services/department-creation.service';
import { UserOperationNotificationService } from '../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
    selector: 'app-department-create-dropdown',
    templateUrl: './department-create-dropdown.component.html',
    styleUrl: './department-create-dropdown.component.scss',
    standalone: false
})
export class DepartmentCreateDropdownComponent implements ISubbmittable {
  @Input({ required: true }) visibility: boolean;
  @Output() visibilityChange: EventEmitter<boolean> = new EventEmitter();
  @Output() departmentAdded: EventEmitter<Department> = new EventEmitter();

  protected name: string = '';

  public constructor(
    private readonly _notificationService: UserOperationNotificationService,
    private readonly _departmentCreateService: DepartmentCreationService,
  ) {}

  public submit(): void {
    if (this.isDepartmentNameEmpty()) return;
    const department = this.createNewDepartment();
    const message = this.createNotificationMessage(department);
    this._departmentCreateService
      .create(department)
      .pipe(
        tap((response) => {
          this._notificationService.SetMessage = message;
          this._notificationService.success();
          this.departmentAdded.emit(department);
          this.close();
        }),
        catchError((error: HttpErrorResponse) => {
          this._notificationService.SetMessage = error.error;
          this._notificationService.failure();
          this.close();
          return new Observable<never>();
        }),
      )
      .subscribe();
  }

  public close(): void {
    this.visibility = false;
    this.visibilityChange.emit(this.visibility);
  }

  private isDepartmentNameEmpty(): boolean {
    if (IsNullOrWhiteSpace(this.name)) {
      this._notificationService.SetMessage = 'Имя кафедры было пустым';
      this._notificationService.failure();
      return true;
    }
    return false;
  }

  private createNewDepartment(): Department {
    const department = {} as Department;
    department.name = this.name;
    return department;
  }

  private createNotificationMessage(department: Department): string {
    return `Добавлена кафедра ${department.name}`;
  }
}
