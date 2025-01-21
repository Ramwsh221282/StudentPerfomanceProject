import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ControlWeekReportInterface } from '../../../Models/Data/control-week-report-interface';
import { ISubbmittable } from '../../../../../models/interfaces/isubbmitable';
import { UserOperationNotificationService } from '../../../../../services/user-notifications/user-operation-notification-service.service';
import { SessionReportItemRemoveService } from './session-report-item-remove.service';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { GreenOutlineButtonComponent } from '../../../../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { RedOutlineButtonComponent } from '../../../../../../building-blocks/buttons/red-outline-button/red-outline-button.component';
import { DatePipe } from '@angular/common';

@Component({
    selector: 'app-session-report-item-remove-modal',
    imports: [GreenOutlineButtonComponent, RedOutlineButtonComponent, DatePipe],
    templateUrl: './session-report-item-remove-modal.component.html',
    styleUrl: './session-report-item-remove-modal.component.scss'
})
export class SessionReportItemRemoveModalComponent implements ISubbmittable {
  @Input({ required: true }) visibility: boolean = false;
  @Input({ required: true }) reportToDelete: ControlWeekReportInterface;
  @Output() visibilityChange: EventEmitter<boolean> = new EventEmitter();
  @Output() reportDeleted: EventEmitter<ControlWeekReportInterface> =
    new EventEmitter();

  public constructor(
    private readonly _notificationService: UserOperationNotificationService,
    private readonly _removeService: SessionReportItemRemoveService,
  ) {}

  public submit(): void {
    this._removeService
      .removeReport(this.reportToDelete)
      .pipe(
        tap((response) => {
          this._notificationService.SetMessage = `Отчёт о контрольной неделе ${this.reportToDelete.season} ${this.reportToDelete.number} ${this.reportToDelete.creationDate} ${this.reportToDelete.completionDate} удалён`;
          this._notificationService.success();
          this.reportDeleted.emit(this.reportToDelete);
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
}
