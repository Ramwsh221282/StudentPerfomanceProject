import { Component, EventEmitter, Output } from '@angular/core';
import { CreateControlWeekService } from './create-control-week.service';
import { NotificationService } from '../../../building-blocks/notifications/notification.service';
import { UnauthorizedErrorHandler } from '../../../shared/models/common/401-error-handler/401-error-handler.service';
import { CurrentControlWeekViewModel } from '../control-week-table.viewmodel';
import { IsNullOrWhiteSpace } from '../../../shared/utils/string-helper';
import { FloatingLabelInputComponent } from '../../../building-blocks/floating-label-input/floating-label-input.component';
import { GreenOutlineButtonComponent } from '../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { RedOutlineButtonComponent } from '../../../building-blocks/buttons/red-outline-button/red-outline-button.component';
import { NgIf } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ControlWeekNumberSelectComponent } from './control-week-number-select/control-week-number-select.component';
import { SeasonWeekSelectComponent } from './season-week-select/season-week-select.component';
import { AssignmentSessionDate } from '../../../modules/administration/submodules/assignment-sessions/models/contracts/assignment-session-contract/assignment-session-date';
import { DateParser } from '../../../shared/models/date-parser/date-parser';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { animate, style, transition, trigger } from '@angular/animations';

@Component({
  selector: 'app-create-control-week-dialog',
  imports: [
    FloatingLabelInputComponent,
    GreenOutlineButtonComponent,
    RedOutlineButtonComponent,
    NgIf,
    ReactiveFormsModule,
    FormsModule,
    ControlWeekNumberSelectComponent,
    SeasonWeekSelectComponent,
  ],
  templateUrl: './create-control-week-dialog.component.html',
  styleUrl: './create-control-week-dialog.component.scss',
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
export class CreateControlWeekDialogComponent {
  @Output() visibilityChanged: EventEmitter<void> = new EventEmitter();

  public startDateInput: string = '';
  public selectedSessionNumber: string = '';
  public selectedSessionSeason: string = '';
  public isSelectingSessionNumber: boolean = false;
  public isSelectingSessionSeason: boolean = false;

  public constructor(
    private readonly _service: CreateControlWeekService,
    private readonly _notifications: NotificationService,
    private readonly _handler: UnauthorizedErrorHandler,
    private readonly _viewModel: CurrentControlWeekViewModel,
  ) {}

  public create(): void {
    if (IsNullOrWhiteSpace(this.selectedSessionSeason)) {
      this._notifications.bulkFailure(
        'Необходимо указать сезон контрольной недели',
      );
      return;
    }
    if (this.selectedSessionNumber == null) {
      this._notifications.bulkFailure(
        'Необходимо выбрать номер контрольной недели',
      );
      return;
    }
    if (IsNullOrWhiteSpace(this.startDateInput)) {
      this._notifications.bulkFailure('Необходимо указать дату');
      return;
    }
    const dateParser: DateParser = new DateParser(this.startDateInput);
    const date: AssignmentSessionDate = {
      day: dateParser.parseDay(),
      month: dateParser.parseMonth(),
      year: dateParser.parseYear(),
    } as AssignmentSessionDate;
    const number = Number(this.selectedSessionNumber);
    this._service
      .create(date, this.selectedSessionSeason, number)
      .pipe(
        tap((response) => {
          this._notifications.bulkSuccess('Создана контрольная неделя');
          this._viewModel.initialize(response);
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
