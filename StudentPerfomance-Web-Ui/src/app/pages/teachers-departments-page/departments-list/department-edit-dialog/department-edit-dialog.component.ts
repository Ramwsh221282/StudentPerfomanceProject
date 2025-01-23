import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Department } from '../../../../modules/administration/submodules/departments/models/departments.interface';
import { DepartmentEditService } from './department-edit.service';
import { NotificationService } from '../../../../building-blocks/notifications/notification.service';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { FloatingLabelInputComponent } from '../../../../building-blocks/floating-label-input/floating-label-input.component';
import { GreenOutlineButtonComponent } from '../../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { RedOutlineButtonComponent } from '../../../../building-blocks/buttons/red-outline-button/red-outline-button.component';
import { SelectDirectionTypeDropdownComponent } from '../../../education-objects-page/education-directions-inline-list/create-education-direction-dialog/select-direction-type-dropdown/select-direction-type-dropdown.component';
import { YellowButtonComponent } from '../../../../building-blocks/buttons/yellow-button/yellow-button.component';

@Component({
  selector: 'app-department-edit-dialog',
  imports: [
    FloatingLabelInputComponent,
    GreenOutlineButtonComponent,
    RedOutlineButtonComponent,
    SelectDirectionTypeDropdownComponent,
    YellowButtonComponent,
  ],
  templateUrl: './department-edit-dialog.component.html',
  styleUrl: './department-edit-dialog.component.scss',
  standalone: true,
})
export class DepartmentEditDialogComponent implements OnInit {
  @Input({ required: true }) department: Department;
  @Output() visibilityChanged: EventEmitter<void> = new EventEmitter();
  public departmentCopy: Department;

  public constructor(
    private readonly _service: DepartmentEditService,
    private readonly _notifications: NotificationService,
  ) {}

  public ngOnInit() {
    this.departmentCopy = { ...this.department };
  }

  public edit(): void {
    this._service
      .edit(this.department, this.departmentCopy)
      .pipe(
        tap(() => {
          this._notifications.setMessage('Изменены данные кафедры');
          this._notifications.success();
          this._notifications.turn();
          this.department.name = this.departmentCopy.name;
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

  public reset(): void {
    this.ngOnInit();
  }
}
