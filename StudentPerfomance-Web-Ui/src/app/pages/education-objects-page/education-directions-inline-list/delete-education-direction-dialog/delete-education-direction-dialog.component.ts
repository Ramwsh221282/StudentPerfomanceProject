import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FloatingLabelInputComponent } from '../../../../building-blocks/floating-label-input/floating-label-input.component';
import { GreenOutlineButtonComponent } from '../../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { RedOutlineButtonComponent } from '../../../../building-blocks/buttons/red-outline-button/red-outline-button.component';
import { SelectDirectionTypeDropdownComponent } from '../create-education-direction-dialog/select-direction-type-dropdown/select-direction-type-dropdown.component';
import { EducationDirection } from '../../../../modules/administration/submodules/education-directions/models/education-direction-interface';
import { RedButtonComponent } from '../../../../building-blocks/buttons/red-button/red-button.component';
import { DeleteEducationDirectionService } from './delete-education-direction.service';
import { NotificationService } from '../../../../building-blocks/notifications/notification.service';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-delete-education-direction-dialog',
  imports: [
    FloatingLabelInputComponent,
    GreenOutlineButtonComponent,
    RedOutlineButtonComponent,
    SelectDirectionTypeDropdownComponent,
    RedButtonComponent,
  ],
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
  ) {}

  public delete(): void {
    this._service
      .delete(this.educationDirection)
      .pipe(
        tap(() => {
          this._notifications.setMessage('Направление подготовки было удалено');
          this._notifications.success();
          this._notifications.turn();
          this.visibilityChanged.emit();
          this.directionRemoved.emit(this.educationDirection);
        }),
        catchError((error: HttpErrorResponse) => {
          this._notifications.setMessage(error.error);
          this._notifications.failure();
          this._notifications.turn();
          this.visibilityChanged.emit();
          return new Observable<never>();
        }),
      )
      .subscribe();
  }
}
