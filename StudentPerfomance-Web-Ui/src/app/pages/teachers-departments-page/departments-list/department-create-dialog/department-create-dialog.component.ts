import { Component, EventEmitter, Output } from '@angular/core';
import { NotificationService } from '../../../../building-blocks/notifications/notification.service';
import { IsNullOrWhiteSpace } from '../../../../shared/utils/string-helper';
import { FloatingLabelInputComponent } from '../../../../building-blocks/floating-label-input/floating-label-input.component';
import { GreenOutlineButtonComponent } from '../../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { RedOutlineButtonComponent } from '../../../../building-blocks/buttons/red-outline-button/red-outline-button.component';
import { SelectDirectionTypeDropdownComponent } from '../../../education-objects-page/education-directions-inline-list/create-education-direction-dialog/select-direction-type-dropdown/select-direction-type-dropdown.component';
import { Department } from '../../../../modules/administration/submodules/departments/models/departments.interface';
import { DepartmentCreateService } from './department-create.service';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-department-create-dialog',
  imports: [
    FloatingLabelInputComponent,
    GreenOutlineButtonComponent,
    RedOutlineButtonComponent,
    SelectDirectionTypeDropdownComponent,
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
  ) {}

  public create(): void {
    if (IsNullOrWhiteSpace(this.name)) {
      this._notifications.setMessage('Название кафедры не должно быть пустым');
      this._notifications.failure();
      this._notifications.turn();
      return;
    }
    const department = {} as Department;
    department.name = this.name;
    this._service
      .create(department)
      .pipe(
        tap((response) => {
          this._notifications.setMessage('Добавлена новая кафедра');
          this._notifications.success();
          this._notifications.turn();
          this.departmentCreated.emit(response);
          this.cleanInputs();
        }),
        catchError((error: HttpErrorResponse) => {
          this._notifications.setMessage(error.error);
          this._notifications.failure();
          this._notifications.turn();
          return new Observable<never>();
        }),
      )
      .subscribe();
  }

  private cleanInputs(): void {
    this.name = '';
  }
}
