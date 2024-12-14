import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ISubbmittable } from '../../../../../../../shared/models/interfaces/isubbmitable';
import { UserOperationNotificationService } from '../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { AssignmentSessionCreateService } from './assignment-session-create.service';
import { DateParser } from '../../../../../../../shared/models/date-parser/date-parser';
import { AssignmentSessionDate } from '../../../models/contracts/assignment-session-contract/assignment-session-date';
import { AssignmentSessionCreateHandler } from './assignment-session-create-handler';
import { catchError, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-assignment-sessions-create-modal',
  templateUrl: './assignment-sessions-create-modal.component.html',
  styleUrl: './assignment-sessions-create-modal.component.scss',
  providers: [AssignmentSessionCreateService],
})
export class AssignmentSessionsCreateModalComponent
  implements OnInit, ISubbmittable
{
  @Output() emitClose: EventEmitter<void> = new EventEmitter();
  @Output() success: EventEmitter<void> = new EventEmitter();
  @Output() failure: EventEmitter<void> = new EventEmitter();

  protected startDateValue: string | null;
  protected season: string;
  protected number: number;

  public constructor(
    private readonly _notificationService: UserOperationNotificationService,
    private readonly _createService: AssignmentSessionCreateService,
  ) {}

  public ngOnInit(): void {
    this.startDateValue = null;
  }

  public submit(): void {
    if (this.startDateValue == null) {
      this._notificationService.SetMessage = 'Период не был указан корректно';
      this.failure.emit();
      return;
    }
    const startDateParser: DateParser = new DateParser(this.startDateValue);
    const startDate: AssignmentSessionDate = {
      day: startDateParser.parseDay(),
      month: startDateParser.parseMonth(),
      year: startDateParser.parseYear(),
    } as AssignmentSessionDate;

    const handler = AssignmentSessionCreateHandler(
      this._notificationService,
      this.success,
      this.failure,
    );
    this._createService
      .create(startDate, this.season, this.number)
      .pipe(
        tap((response) => handler.handle(response)),
        catchError((error: HttpErrorResponse) => handler.handleError(error)),
      )
      .subscribe();
  }
}
