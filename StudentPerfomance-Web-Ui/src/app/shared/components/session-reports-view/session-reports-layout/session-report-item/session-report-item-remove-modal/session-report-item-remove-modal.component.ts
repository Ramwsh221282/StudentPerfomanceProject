import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ControlWeekReportInterface } from '../../../Models/Data/control-week-report-interface';
import { ISubbmittable } from '../../../../../models/interfaces/isubbmitable';
import { UserOperationNotificationService } from '../../../../../services/user-notifications/user-operation-notification-service.service';
import { SessionReportItemRemoveService } from './session-report-item-remove.service';
import { catchError, Observable, tap } from 'rxjs';

@Component({
  selector: 'app-session-report-item-remove-modal',
  standalone: true,
  imports: [],
  templateUrl: './session-report-item-remove-modal.component.html',
  styleUrl: './session-report-item-remove-modal.component.scss',
})
export class SessionReportItemRemoveModalComponent implements ISubbmittable {
  @Input({ required: true }) reportToDelete: ControlWeekReportInterface;
  @Output() visibility: EventEmitter<void> = new EventEmitter();
  @Output() refresh: EventEmitter<void> = new EventEmitter();
  @Output() successEmitter: EventEmitter<void> = new EventEmitter();
  @Output() failureEmitter: EventEmitter<void> = new EventEmitter();

  public constructor(
    private readonly _notificationService: UserOperationNotificationService,
    private readonly _removeService: SessionReportItemRemoveService,
  ) {}

  public submit(): void {
    this._removeService
      .removeReport(this.reportToDelete)
      .pipe(
        tap((response) => {
          this._notificationService.SetMessage = `Отчёт о контрольной неделе ${this.reportToDelete.creationDate} - ${this.reportToDelete.completionDate} удалён`;
          this.refresh.emit();
          this.successEmitter.emit();
          this.visibility.emit();
        }),
        catchError((error) => {
          this._notificationService.SetMessage = error.error;
          this.failureEmitter.emit();
          this.visibility.emit();
          return new Observable();
        }),
      )
      .subscribe();
  }
}
