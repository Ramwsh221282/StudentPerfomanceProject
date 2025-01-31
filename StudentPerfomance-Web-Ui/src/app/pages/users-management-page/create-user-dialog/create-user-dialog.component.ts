import { Component, EventEmitter, Output } from '@angular/core';
import { NotificationService } from '../../../building-blocks/notifications/notification.service';
import { UnauthorizedErrorHandler } from '../../../shared/models/common/401-error-handler/401-error-handler.service';
import { UsersPageViewmodel } from '../users-page.viewmodel';
import { CreateUserService } from './create-user.service';
import { IsNullOrWhiteSpace } from '../../../shared/utils/string-helper';
import { UserRecord } from '../../../modules/administration/submodules/users/services/user-table-element-interface';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { FloatingLabelInputComponent } from '../../../building-blocks/floating-label-input/floating-label-input.component';
import { GreenOutlineButtonComponent } from '../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { RedOutlineButtonComponent } from '../../../building-blocks/buttons/red-outline-button/red-outline-button.component';

@Component({
  selector: 'app-create-user-dialog',
  imports: [
    FloatingLabelInputComponent,
    GreenOutlineButtonComponent,
    RedOutlineButtonComponent,
  ],
  templateUrl: './create-user-dialog.component.html',
  styleUrl: './create-user-dialog.component.scss',
  standalone: true,
})
export class CreateUserDialogComponent {
  @Output() visibilityChanged: EventEmitter<void> = new EventEmitter();

  public name: string = '';
  public surname: string = '';
  public patronymic: string = '';
  public email: string = '';

  public constructor(
    private readonly _notifications: NotificationService,
    private readonly _handler: UnauthorizedErrorHandler,
    private readonly _viewModel: UsersPageViewmodel,
    private readonly _service: CreateUserService,
  ) {}

  public create(): void {
    if (IsNullOrWhiteSpace(this.name)) {
      this._notifications.bulkFailure('Имя пользователя должно быть указано');
      return;
    }
    if (IsNullOrWhiteSpace(this.surname)) {
      this._notifications.bulkFailure(
        'Фамилия пользователя должна быть указана',
      );
      return;
    }
    if (IsNullOrWhiteSpace(this.email)) {
      this._notifications.bulkFailure('Почта пользователя должна быть указана');
    }
    const userPayload: UserRecord = {} as UserRecord;
    userPayload.name = this.name;
    userPayload.patronymic = this.patronymic;
    userPayload.surname = this.surname;
    userPayload.email = this.email;
    userPayload.role = 'Администратор';
    this._service
      .create(userPayload)
      .pipe(
        tap((response) => {
          this._notifications.bulkSuccess('Добавлен новый администратор');
          this._viewModel.addUser(response);
          this.resetInputs();
        }),
        catchError((error: HttpErrorResponse) => {
          this._handler.tryHandle(error);
          this._notifications.bulkFailure(error.error);
          return new Observable<never>();
        }),
      )
      .subscribe();
  }

  private resetInputs(): void {
    this.name = '';
    this.surname = '';
    this.patronymic = '';
    this.email = '';
  }
}
