import { Component, EventEmitter, Input, Output } from '@angular/core';
import { StudentGroup } from '../../services/studentsGroup.interface';
import { ISubbmittable } from '../../../../../../shared/models/interfaces/isubbmitable';
import { StudentGroupsFacadeService } from '../../services/student-groups-facade.service';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
    selector: 'app-remove-group-popup',
    templateUrl: './remove-group-popup.component.html',
    styleUrl: './remove-group-popup.component.scss',
    standalone: false
})
export class RemoveGroupPopupComponent implements ISubbmittable {
  @Input({ required: true }) visibility: boolean = false;
  @Input() group: StudentGroup;
  @Output() visibilityChange: EventEmitter<boolean> = new EventEmitter();
  @Output() groupRemoveCommited: EventEmitter<StudentGroup> =
    new EventEmitter();

  public constructor(
    private readonly _facadeService: StudentGroupsFacadeService,
    private readonly _notificationService: UserOperationNotificationService,
  ) {}

  public submit(): void {
    this._facadeService
      .delete(this.group)
      .pipe(
        tap((response) => {
          this.groupRemoveCommited.emit(response);
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
