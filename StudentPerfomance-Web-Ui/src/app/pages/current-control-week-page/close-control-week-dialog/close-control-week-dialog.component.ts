import { Component, EventEmitter, Input, Output } from '@angular/core';
import { AssignmentSession } from '../../../modules/administration/submodules/assignment-sessions/models/assignment-session-interface';
import { NotificationService } from '../../../building-blocks/notifications/notification.service';
import { CloseControlWeekService } from './close-control-week.service';
import { UnauthorizedErrorHandler } from '../../../shared/models/common/401-error-handler/401-error-handler.service';
import { CurrentControlWeekViewModel } from '../control-week-table.viewmodel';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { RedButtonComponent } from '../../../building-blocks/buttons/red-button/red-button.component';
import { RedOutlineButtonComponent } from '../../../building-blocks/buttons/red-outline-button/red-outline-button.component';
import { animate, style, transition, trigger } from '@angular/animations';

@Component({
  selector: 'app-close-control-week-dialog',
  imports: [RedButtonComponent, RedOutlineButtonComponent],
  templateUrl: './close-control-week-dialog.component.html',
  styleUrl: './close-control-week-dialog.component.scss',
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
export class CloseControlWeekDialogComponent {
  @Output() visibilityChanged: EventEmitter<void> = new EventEmitter();
  @Input({ required: true }) session: AssignmentSession;

  public constructor(
    private readonly _notifications: NotificationService,
    private readonly _service: CloseControlWeekService,
    private readonly _handler: UnauthorizedErrorHandler,
    private readonly _viewModel: CurrentControlWeekViewModel,
  ) {}

  public close(): void {
    this._service
      .close(this.session)
      .pipe(
        tap(() => {
          this._notifications.bulkSuccess('Контрольная неделя закрыта');
          this._viewModel.closeSession();
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
