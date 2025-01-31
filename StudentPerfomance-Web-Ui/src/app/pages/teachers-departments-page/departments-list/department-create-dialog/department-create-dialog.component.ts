import { Component, EventEmitter, Output } from '@angular/core';
import { NotificationService } from '../../../../building-blocks/notifications/notification.service';
import { IsNullOrWhiteSpace } from '../../../../shared/utils/string-helper';
import { FloatingLabelInputComponent } from '../../../../building-blocks/floating-label-input/floating-label-input.component';
import { GreenOutlineButtonComponent } from '../../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { RedOutlineButtonComponent } from '../../../../building-blocks/buttons/red-outline-button/red-outline-button.component';
import { Department } from '../../../../modules/administration/submodules/departments/models/departments.interface';
import { DepartmentCreateService } from './department-create.service';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { UnauthorizedErrorHandler } from '../../../../shared/models/common/401-error-handler/401-error-handler.service';

@Component({
  selector: 'app-department-create-dialog',
  imports: [
    FloatingLabelInputComponent,
    GreenOutlineButtonComponent,
    RedOutlineButtonComponent,
  ],
  templateUrl: './department-create-dialog.component.html',
  styleUrl: './department-create-dialog.component.scss',
  standalone: true,
})
export class DepartmentCreateDialogComponent {
  @Output() departmentCreated: EventEmitter<Department> = new EventEmitter();
  @Output() visibilityChanged: EventEmitter<void> = new EventEmitter();
  public name: string = '';

  public constructor(
    private readonly _service: DepartmentCreateService,
    private readonly _notifications: NotificationService,
    private readonly _handler: UnauthorizedErrorHandler,
  ) {}

  public create(): void {
    if (IsNullOrWhiteSpace(this.name)) {
      this._notifications.bulkFailure('Название кафедры не должно быть пустым');
      return;
    }
    const department = {} as Department;
    department.name = this.name;
    this._service
      .create(department)
      .pipe(
        tap((response) => {
          this._notifications.bulkSuccess('Добавлена новая кафедра');
          this.departmentCreated.emit(response);
          this.cleanInputs();
        }),
        catchError((error: HttpErrorResponse) => {
          this._handler.tryHandle(error);
          this._notifications.bulkFailure(error.error);
          return new Observable<never>();
        }),
      )
      .subscribe();
  }

  private cleanInputs(): void {
    this.name = '';
  }
}
