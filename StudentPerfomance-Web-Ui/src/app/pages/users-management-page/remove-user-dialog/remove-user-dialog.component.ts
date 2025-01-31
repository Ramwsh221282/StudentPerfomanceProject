import { Component, EventEmitter, Input, Output } from '@angular/core';
import { UserRecord } from '../../../modules/administration/submodules/users/services/user-table-element-interface';
import { NotificationService } from '../../../building-blocks/notifications/notification.service';
import { UnauthorizedErrorHandler } from '../../../shared/models/common/401-error-handler/401-error-handler.service';
import { RemoveUserService } from './remove-user.service';
import { UsersPageViewmodel } from '../users-page.viewmodel';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { GreenOutlineButtonComponent } from '../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { RedOutlineButtonComponent } from '../../../building-blocks/buttons/red-outline-button/red-outline-button.component';
import { NgIf } from '@angular/common';
import { AuthService } from '../../user-page/services/auth.service';
import { animate, style, transition, trigger } from '@angular/animations';

@Component({
  selector: 'app-remove-user-dialog',
  imports: [GreenOutlineButtonComponent, RedOutlineButtonComponent, NgIf],
  templateUrl: './remove-user-dialog.component.html',
  styleUrl: './remove-user-dialog.component.scss',
  standalone: true,
  animations: [
    trigger('fadeIn', [
      transition(':enter', [
        style({ opacity: 0, transform: 'translateY(-10px)' }),
        animate(
          '300ms ease-out',
          style({ opacity: 1, transform: 'translateY(0)' }),
        ),
      ]),
      transition(':leave', [
        animate(
          '300ms ease-in',
          style({ opacity: 0, transform: 'translateY(-10px)' }),
        ),
      ]),
    ]),
  ],
})
export class RemoveUserDialogComponent {
  @Output() visibilityChanged: EventEmitter<void> = new EventEmitter();
  @Input({ required: true }) userRecord: UserRecord;

  public constructor(
    private readonly _notifications: NotificationService,
    private readonly _handler: UnauthorizedErrorHandler,
    private readonly _service: RemoveUserService,
    private readonly _viewModel: UsersPageViewmodel,
    private readonly _auth: AuthService,
  ) {}

  public remove(): void {
    const currentUserData = this._auth.userData;
    if (
      currentUserData.name == this.userRecord.name &&
      currentUserData.surname == this.userRecord.surname &&
      currentUserData.patronymic == this.userRecord.patronymic &&
      currentUserData.email == this.userRecord.email &&
      currentUserData.role == this.userRecord.role
    ) {
      this._notifications.bulkSuccess('Нельзя удалить самого себя');
      this.visibilityChanged.emit();
      return;
    }
    this._service
      .remove(this.userRecord)
      .pipe(
        tap(() => {
          this._notifications.bulkSuccess('Пользователь был удалён');
          this._viewModel.removeUser(this.userRecord);
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
}
