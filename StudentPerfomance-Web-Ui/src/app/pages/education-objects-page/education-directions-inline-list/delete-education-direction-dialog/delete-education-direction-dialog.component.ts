import { Component, EventEmitter, Input, Output } from '@angular/core';
import { RedOutlineButtonComponent } from '../../../../building-blocks/buttons/red-outline-button/red-outline-button.component';
import { EducationDirection } from '../../../../modules/administration/submodules/education-directions/models/education-direction-interface';
import { RedButtonComponent } from '../../../../building-blocks/buttons/red-button/red-button.component';
import { DeleteEducationDirectionService } from './delete-education-direction.service';
import { NotificationService } from '../../../../building-blocks/notifications/notification.service';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { UnauthorizedErrorHandler } from '../../../../shared/models/common/401-error-handler/401-error-handler.service';

@Component({
  selector: 'app-delete-education-direction-dialog',
  imports: [RedOutlineButtonComponent, RedButtonComponent],
  templateUrl: './delete-education-direction-dialog.component.html',
  styleUrl: './delete-education-direction-dialog.component.scss',
  standalone: true,
})
export class DeleteEducationDirectionDialogComponent {
  @Input({ required: true }) educationDirection: EducationDirection;
  @Output() directionRemoved: EventEmitter<EducationDirection> =
    new EventEmitter();
  @Output() visibilityChanged: EventEmitter<void> = new EventEmitter();

  public constructor(
    private readonly _service: DeleteEducationDirectionService,
    private readonly _notifications: NotificationService,
    private readonly _handler: UnauthorizedErrorHandler,
  ) {}

  public delete(): void {
    this._service
      .delete(this.educationDirection)
      .pipe(
        tap(() => {
          this._notifications.bulkSuccess(
            'Направление подготовки было удалено',
          );
          this.visibilityChanged.emit();
          this.directionRemoved.emit(this.educationDirection);
        }),
        catchError((error: HttpErrorResponse) => {
          this._handler.tryHandle(error);
          this._notifications.bulkFailure(error.error);
          this.visibilityChanged.emit();
          return new Observable<never>();
        }),
      )
      .subscribe();
  }
}
