import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Department } from '../../../../modules/administration/submodules/departments/models/departments.interface';
import { NotificationService } from '../../../../building-blocks/notifications/notification.service';
import { DepartmentRemoveService } from './department-remove.service';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { RedButtonComponent } from '../../../../building-blocks/buttons/red-button/red-button.component';
import { RedOutlineButtonComponent } from '../../../../building-blocks/buttons/red-outline-button/red-outline-button.component';

@Component({
  selector: 'app-department-remove-dialog',
  imports: [RedButtonComponent, RedOutlineButtonComponent],
  templateUrl: './department-remove-dialog.component.html',
  styleUrl: './department-remove-dialog.component.scss',
  standalone: true,
})
export class DepartmentRemoveDialogComponent {
  @Input({ required: true }) department: Department;
  @Output() visibilityChanged: EventEmitter<void> = new EventEmitter();
  @Output() departmentRemoved: EventEmitter<Department> = new EventEmitter();

  public constructor(
    private readonly _service: DepartmentRemoveService,
    private readonly _notifications: NotificationService,
  ) {}

  public remove(): void {
    this._service
      .remove(this.department)
      .pipe(
        tap(() => {
          this._notifications.setMessage('Кафедра была удалена');
          this._notifications.success();
          this._notifications.turn();
          this.departmentRemoved.emit(this.department);
          this.visibilityChanged.emit();
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
}
