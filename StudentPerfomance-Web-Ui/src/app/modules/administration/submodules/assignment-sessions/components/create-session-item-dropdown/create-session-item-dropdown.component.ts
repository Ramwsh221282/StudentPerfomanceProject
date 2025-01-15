import { Component, EventEmitter, Input, Output } from '@angular/core';
import { IsNullOrWhiteSpace } from '../../../../../../shared/utils/string-helper';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { AssignmentSession } from '../../models/assignment-session-interface';
import { AssignmentSessionCreateService } from '../../services/assignment-session-create.service';
import { DateParser } from '../../../../../../shared/models/date-parser/date-parser';
import { AssignmentSessionDate } from '../../models/contracts/assignment-session-contract/assignment-session-date';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-create-session-item-dropdown',
  templateUrl: './create-session-item-dropdown.component.html',
  styleUrl: './create-session-item-dropdown.component.scss',
  providers: [AssignmentSessionCreateService],
})
export class CreateSessionItemDropdownComponent {
  @Input({ required: true }) visibility: boolean = false;
  @Output() visibilityChange: EventEmitter<boolean> = new EventEmitter();
  @Output() sessionCreated: EventEmitter<AssignmentSession> =
    new EventEmitter();

  protected startDateInput: string = '';

  protected selectedSessionNumber: number | null = null;
  protected selectSessionNumberLabel: string = 'Выберите номер';
  protected isSelectingSessionNumber: boolean = false;

  protected selectedSessionSeason: string | null = null;
  protected selectSessionSeasonLabel: string = 'Выберите сезон';
  protected isSelectingSessionSeason: boolean = false;

  protected readonly sessionNumbers: string[] = ['1', '2', '3', '4'];
  protected readonly sessionSeasons: string[] = ['Весна', 'Осень'];

  public constructor(
    private readonly _notificationService: UserOperationNotificationService,
    private readonly _createService: AssignmentSessionCreateService,
  ) {}

  public submit(): void {
    if (this.isDateEmpty()) return;
    if (this.isNumberEmpty()) return;
    if (this.isSeasonEmpty()) return;
    const startDateParser: DateParser = new DateParser(this.startDateInput);
    const date: AssignmentSessionDate = {
      day: startDateParser.parseDay(),
      month: startDateParser.parseMonth(),
      year: startDateParser.parseYear(),
    } as AssignmentSessionDate;
    this._createService
      .create(date, this.selectedSessionSeason!, this.selectedSessionNumber!)
      .pipe(
        tap((response) => {
          this._notificationService.SetMessage = `Контрольная неделя создана`;
          this._notificationService.success();
          this.sessionCreated.emit(response);
          this.close();
        }),
        catchError((error: HttpErrorResponse) => {
          this._notificationService.SetMessage = error.error;
          this._notificationService.failure();
          this.close();
          return new Observable<never>();
        }),
      )
      .subscribe();
  }

  protected close(): void {
    this.visibility = false;
    this.visibilityChange.emit(this.visibility);
  }

  protected handleNumberSelected(number: string): void {
    this.selectedSessionNumber = Number(number);
    this.selectSessionNumberLabel = number;
  }

  protected handleSeasonSelected(season: string): void {
    this.selectedSessionSeason = season;
    this.selectSessionSeasonLabel = season;
  }

  private isDateEmpty(): boolean {
    if (IsNullOrWhiteSpace(this.startDateInput)) {
      this._notificationService.SetMessage =
        'Дата начала контрольной недели не указана';
      this._notificationService.failure();
      return true;
    }
    return false;
  }

  private isNumberEmpty(): boolean {
    if (!this.selectedSessionNumber) {
      this._notificationService.SetMessage =
        'Номер контрольной недели не указан';
      this._notificationService.failure();
      return true;
    }
    return false;
  }

  private isSeasonEmpty(): boolean {
    if (IsNullOrWhiteSpace(this.selectedSessionSeason)) {
      this._notificationService.SetMessage =
        'Сезон контрольной недели не указан';
      this._notificationService.failure();
      return true;
    }
    return false;
  }
}
