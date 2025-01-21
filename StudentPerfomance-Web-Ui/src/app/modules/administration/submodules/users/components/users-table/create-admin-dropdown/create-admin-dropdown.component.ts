import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ISubbmittable } from '../../../../../../../shared/models/interfaces/isubbmitable';
import { UserOperationNotificationService } from '../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { UserCreationService } from '../../../services/user-create.service';
import { IsNullOrWhiteSpace } from '../../../../../../../shared/utils/string-helper';
import { User } from '../../../../../../../pages/user-page/services/user-interface';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-create-admin-dropdown',
  templateUrl: './create-admin-dropdown.component.html',
  styleUrl: './create-admin-dropdown.component.scss',
  providers: [UserCreationService],
})
export class CreateAdminDropdownComponent implements ISubbmittable {
  @Input({ required: true }) visibility: boolean = false;
  @Output() visibilityChange: EventEmitter<boolean> =
    new EventEmitter<boolean>();
  @Output() userCreated: EventEmitter<User> = new EventEmitter();

  private readonly _role: string = 'Администратор';
  protected userName: string = '';
  protected userSurname: string = '';
  protected userPatronymic: string = '';
  protected userEmail: string = '';

  public constructor(
    private readonly _notificationService: UserOperationNotificationService,
    private readonly _createService: UserCreationService,
  ) {}

  public submit(): void {
    if (this.isUserNameEmpty()) return;
    if (this.isUserSurnameEmpty()) return;
    if (this.isUserEmailEmpty()) return;
    const user = this.createNewUser();
    this._createService
      .create(user)
      .pipe(
        tap((response) => {
          this._notificationService.SetMessage = `Зарегистрирован новый администратор ${user.surname} ${user.name[0]} ${user.patronymic == null ? '' : user.patronymic[0]}`;
          this._notificationService.success();
          this.userCreated.emit(user);
          this.cleanInputs();
        }),
        catchError((error: HttpErrorResponse) => {
          this._notificationService.SetMessage = error.error;
          this._notificationService.failure();
          return new Observable<never>();
        }),
      )
      .subscribe();
  }

  protected closeDropdown(): void {
    this.visibility = false;
    this.visibilityChange.emit(this.visibility);
  }

  private isUserNameEmpty(): boolean {
    if (IsNullOrWhiteSpace(this.userName)) {
      this._notificationService.SetMessage = 'Имя было пустым';
      this._notificationService.failure();
      return true;
    }
    return false;
  }

  private isUserSurnameEmpty(): boolean {
    if (IsNullOrWhiteSpace(this.userSurname)) {
      this._notificationService.SetMessage = 'Фамилия была пустой';
      this._notificationService.failure();
      return true;
    }
    return false;
  }

  private isUserEmailEmpty(): boolean {
    if (IsNullOrWhiteSpace(this.userEmail)) {
      this._notificationService.SetMessage = 'Почта была пустой';
      this._notificationService.failure();
      return true;
    }
    return false;
  }

  private createNewUser(): User {
    const user = {} as User;
    user.name = this.userName;
    user.surname = this.userSurname;
    user.patronymic = this.userPatronymic;
    user.role = this._role;
    user.email = this.userEmail;
    return user;
  }

  private cleanInputs(): void {
    this.userName = '';
    this.userSurname = '';
    this.userPatronymic = '';
    this.userEmail = '';
  }
}
