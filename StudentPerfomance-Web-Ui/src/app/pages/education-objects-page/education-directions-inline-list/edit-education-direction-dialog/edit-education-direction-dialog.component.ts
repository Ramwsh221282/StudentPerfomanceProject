import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FloatingLabelInputComponent } from '../../../../building-blocks/floating-label-input/floating-label-input.component';
import { GreenOutlineButtonComponent } from '../../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { RedOutlineButtonComponent } from '../../../../building-blocks/buttons/red-outline-button/red-outline-button.component';
import { SelectDirectionTypeDropdownComponent } from '../create-education-direction-dialog/select-direction-type-dropdown/select-direction-type-dropdown.component';
import { EducationDirection } from '../../../../modules/administration/submodules/education-directions/models/education-direction-interface';
import { YellowButtonComponent } from '../../../../building-blocks/buttons/yellow-button/yellow-button.component';
import { EditEducationDirectionService } from './edit-education-direction.service';
import { NotificationService } from '../../../../building-blocks/notifications/notification.service';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { UnauthorizedErrorHandler } from '../../../../shared/models/common/401-error-handler/401-error-handler.service';

@Component({
  selector: 'app-edit-education-direction-dialog',
  imports: [
    FloatingLabelInputComponent,
    GreenOutlineButtonComponent,
    RedOutlineButtonComponent,
    SelectDirectionTypeDropdownComponent,
    YellowButtonComponent,
  ],
  templateUrl: './edit-education-direction-dialog.component.html',
  styleUrl: './edit-education-direction-dialog.component.scss',
  standalone: true,
})
export class EditEducationDirectionDialogComponent implements OnInit {
  @Output() dialogClose: EventEmitter<boolean> = new EventEmitter();
  @Input({ required: true }) educationDirection: EducationDirection;
  public educationDirectionCopy: EducationDirection;
  public isSelectingType: boolean = false;

  constructor(
    private readonly _service: EditEducationDirectionService,
    private readonly _notifications: NotificationService,
    private readonly _handler: UnauthorizedErrorHandler,
  ) {}

  public ngOnInit() {
    this.educationDirectionCopy = { ...this.educationDirection };
  }

  public edit(): void {
    this._service
      .update(this.educationDirection, this.educationDirectionCopy)
      .pipe(
        tap(() => {
          this.educationDirection.name = this.educationDirectionCopy.name;
          this.educationDirection.code = this.educationDirectionCopy.code;
          this.educationDirection.type = this.educationDirectionCopy.type;
          this._notifications.bulkSuccess(
            'Данные направления подготовки изменены',
          );
          this.dialogClose.emit(false);
        }),
        catchError((error: HttpErrorResponse) => {
          this._handler.tryHandle(error);
          this._notifications.bulkFailure(error.error);
          return new Observable<never>();
        }),
      )
      .subscribe();
  }

  public reset(): void {
    this.ngOnInit();
  }
}
