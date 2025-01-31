import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UserRecord } from '../../../modules/administration/submodules/users/services/user-table-element-interface';
import { EditUserService } from './edit-user.service';
import { NotificationService } from '../../../building-blocks/notifications/notification.service';
import { UnauthorizedErrorHandler } from '../../../shared/models/common/401-error-handler/401-error-handler.service';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { FloatingLabelInputComponent } from '../../../building-blocks/floating-label-input/floating-label-input.component';
import { GreenOutlineButtonComponent } from '../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { RedOutlineButtonComponent } from '../../../building-blocks/buttons/red-outline-button/red-outline-button.component';
import { RedButtonComponent } from '../../../building-blocks/buttons/red-button/red-button.component';

@Component({
  selector: 'app-edit-user-dialog',
  imports: [
    FloatingLabelInputComponent,
    GreenOutlineButtonComponent,
    RedOutlineButtonComponent,
    RedButtonComponent,
  ],
  templateUrl: './edit-user-dialog.component.html',
  styleUrl: './edit-user-dialog.component.scss',
})
export class EditUserDialogComponent implements OnInit {
  @Input({ required: true }) initial: UserRecord;
  @Output() visibilityChanged: EventEmitter<void> = new EventEmitter();
  public copy: UserRecord;

  constructor(
    private readonly _service: EditUserService,
    private readonly _notifications: NotificationService,
    private readonly _handler: UnauthorizedErrorHandler,
  ) {}

  public ngOnInit() {
    this.copy = { ...this.initial };
  }

  public edit(): void {
    this._service
      .updateUser(this.initial, this.copy)
      .pipe(
        tap(() => {
          this._notifications.bulkSuccess('Данные о пользователе изменены');
          this.initial.name = this.copy.name;
          this.initial.surname = this.copy.surname;
          this.initial.patronymic = this.copy.patronymic;
          this.visibilityChanged.emit();
        }),
        catchError((error: HttpErrorResponse) => {
          this._handler.tryHandle(error);
          this._notifications.bulkFailure(error.error);
          return new Observable<never>();
        }),
      )
      .subscribe();
  }

  public reset(): void {
    this.ngOnInit();
  }
}
