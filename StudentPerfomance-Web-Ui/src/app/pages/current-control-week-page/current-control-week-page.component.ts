import { Component, OnInit } from '@angular/core';
import { NotificationService } from '../../building-blocks/notifications/notification.service';
import { CurrentControlWeekDataService } from './current-control-week-data.service';
import { UnauthorizedErrorHandler } from '../../shared/models/common/401-error-handler/401-error-handler.service';
import { catchError, Observable, tap } from 'rxjs';
import { CurrentControlWeekViewModel } from './control-week-table.viewmodel';
import { HttpErrorResponse } from '@angular/common/http';
import { ControlWeekTableInfoComponent } from './control-week-table-info/control-week-table-info.component';
import { SuccessNotificationComponent } from '../../building-blocks/notifications/success-notification/success-notification.component';
import { NgIf } from '@angular/common';
import { FailureNotificationComponent } from '../../building-blocks/notifications/failure-notification/failure-notification.component';
import { ControlWeekItemComponent } from './control-week-table-info/control-week-item/control-week-item.component';
import { AssignmentSession } from '../../modules/administration/submodules/assignment-sessions/models/assignment-session-interface';
import { CloseControlWeekDialogComponent } from './close-control-week-dialog/close-control-week-dialog.component';
import { CreateControlWeekDialogComponent } from './create-control-week-dialog/create-control-week-dialog.component';
import { animate, style, transition, trigger } from '@angular/animations';

@Component({
  selector: 'app-current-control-week-page',
  imports: [
    ControlWeekTableInfoComponent,
    SuccessNotificationComponent,
    NgIf,
    FailureNotificationComponent,
    ControlWeekItemComponent,
    CloseControlWeekDialogComponent,
    CreateControlWeekDialogComponent,
  ],
  templateUrl: './current-control-week-page.component.html',
  styleUrl: './current-control-week-page.component.scss',
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
export class CurrentControlWeekPageComponent implements OnInit {
  public closeAssignmentSessionRequest: AssignmentSession | null = null;
  public isCreatingNewSession: boolean = false;

  public constructor(
    protected readonly notifications: NotificationService,
    protected readonly viewModel: CurrentControlWeekViewModel,
    private readonly _service: CurrentControlWeekDataService,
    private readonly _handler: UnauthorizedErrorHandler,
  ) {}

  public ngOnInit(): void {
    this._service
      .getCurrent()
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
