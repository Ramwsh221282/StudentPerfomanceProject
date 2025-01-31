import { Component, OnInit } from '@angular/core';
import { UsersPageViewmodel } from './users-page.viewmodel';
import { NotificationService } from '../../building-blocks/notifications/notification.service';
import { UsersDataService } from './users-data.service';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { UnauthorizedErrorHandler } from '../../shared/models/common/401-error-handler/401-error-handler.service';
import { UsersManagementListComponent } from './users-management-list/users-management-list.component';
import { FailureNotificationComponent } from '../../building-blocks/notifications/failure-notification/failure-notification.component';
import { NgIf } from '@angular/common';
import { UsersManagementUserFormComponent } from './users-management-user-form/users-management-user-form.component';
import { UserRecord } from '../../modules/administration/submodules/users/services/user-table-element-interface';
import { CreateUserDialogComponent } from './create-user-dialog/create-user-dialog.component';
import { SuccessNotificationComponent } from '../../building-blocks/notifications/success-notification/success-notification.component';
import { RemoveUserDialogComponent } from './remove-user-dialog/remove-user-dialog.component';
import { EditUserDialogComponent } from './edit-user-dialog/edit-user-dialog.component';
import { animate, style, transition, trigger } from '@angular/animations';

@Component({
  selector: 'app-users-management-page',
  imports: [
    UsersManagementListComponent,
    FailureNotificationComponent,
    NgIf,
    UsersManagementUserFormComponent,
    CreateUserDialogComponent,
    SuccessNotificationComponent,
    RemoveUserDialogComponent,
    EditUserDialogComponent,
  ],
  templateUrl: './users-management-page.component.html',
  styleUrl: './users-management-page.component.scss',
  standalone: true,
  providers: [NotificationService],
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
export class UsersManagementPageComponent implements OnInit {
  public removeUserRequest: UserRecord | null = null;
  public editUserRequest: UserRecord | null;
  public isCreatingNewUser: boolean = false;

  public constructor(
    private readonly _service: UsersDataService,
    private readonly _handler: UnauthorizedErrorHandler,
    protected readonly notifications: NotificationService,
    protected readonly viewModel: UsersPageViewmodel,
  ) {}

  public ngOnInit() {
    this._service
      .getUsers()
      .pipe(
        tap((response) => this.viewModel.initialize(response)),
        catchError((error: HttpErrorResponse) => {
          this._handler.tryHandle(error);
          return new Observable<never>();
        }),
      )
      .subscribe();
  }
}
