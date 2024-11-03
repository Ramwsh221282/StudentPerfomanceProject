import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ISubbmittable } from '../../../../../../../shared/models/interfaces/isubbmitable';
import { DateParser } from '../../../../../../../shared/models/date-parser/date-parser';
import { UserOperationNotificationService } from '../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { AssignmentSessionDate } from '../../../models/contracts/assignment-session-contract/assignment-session-date';
import { AssignmentSessionCreateService } from './assignment-session-create.service';
import { catchError, tap } from 'rxjs';
import { AssignmentSessionCreateHandler } from './assignment-session-create-handler';
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
  protected endDateValue: string | null;

  public constructor(
    private readonly _notificationService: UserOperationNotificationService,
    private readonly _createService: AssignmentSessionCreateService
  ) {}

  public ngOnInit(): void {
    this.startDateValue = null;
    this.endDateValue = null;
  }

  public submit(): void {
    if (this.startDateValue == null || this.endDateValue == null) return;
    const startDateParser: DateParser = new DateParser(this.startDateValue);
    const endDateParser: DateParser = new DateParser(this.endDateValue);
    const startDate: AssignmentSessionDate = {
      day: startDateParser.parseDay(),
      month: startDateParser.parseMonth(),
      year: startDateParser.parseYear(),
    } as AssignmentSessionDate;
    const endDate: AssignmentSessionDate = {
      day: endDateParser.parseDay(),
      month: endDateParser.parseMonth(),
      year: endDateParser.parseYear(),
    } as AssignmentSessionDate;

    const handler = AssignmentSessionCreateHandler(
      this._notificationService,
      this.success,
      this.failure
    );
    this._createService
      .create(startDate, endDate)
      .pipe(
        tap((response) => handler.handle(response)),
        catchError((error: HttpErrorResponse) => handler.handleError(error))
      )
      .subscribe();
  }
}
