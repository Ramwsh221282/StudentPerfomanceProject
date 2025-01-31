import { Component, EventEmitter, Input, Output } from '@angular/core';
import { EducationDirection } from '../../../../modules/administration/submodules/education-directions/models/education-direction-interface';
import { GreenOutlineButtonComponent } from '../../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { RedOutlineButtonComponent } from '../../../../building-blocks/buttons/red-outline-button/red-outline-button.component';
import { FloatingLabelInputComponent } from '../../../../building-blocks/floating-label-input/floating-label-input.component';
import { SelectDirectionTypeDropdownComponent } from './select-direction-type-dropdown/select-direction-type-dropdown.component';
import { CreateEducationDirectionService } from './create-education-direction.service';
import { NotificationService } from '../../../../building-blocks/notifications/notification.service';
import { IsNullOrWhiteSpace } from '../../../../shared/utils/string-helper';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { UnauthorizedErrorHandler } from '../../../../shared/models/common/401-error-handler/401-error-handler.service';
import { animate, style, transition, trigger } from '@angular/animations';

@Component({
  selector: 'app-create-education-direction-dialog',
  templateUrl: './create-education-direction-dialog.component.html',
  styleUrl: './create-education-direction-dialog.component.scss',
  standalone: true,
  imports: [
    GreenOutlineButtonComponent,
    RedOutlineButtonComponent,
    FloatingLabelInputComponent,
    SelectDirectionTypeDropdownComponent,
  ],
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
export class CreateEducationDirectionDialogComponent {
  @Output() directionCreated: EventEmitter<EducationDirection> =
    new EventEmitter();
  @Output() dialogClose: EventEmitter<boolean> = new EventEmitter();
  @Output() visibilityChange: EventEmitter<boolean> = new EventEmitter();
  @Input({ required: true }) visibility: boolean = false;

  constructor(
    private readonly _service: CreateEducationDirectionService,
    private readonly _notifications: NotificationService,
    private readonly _handler: UnauthorizedErrorHandler,
  ) {}

  public directionName: string = '';
  public directionCode: string = '';
  public directionType: string = '';
  public isSelectingType: boolean = false;

  public create(): void {
    if (IsNullOrWhiteSpace(this.directionName)) {
      this._notifications.bulkFailure('Наименование не должно быть пустым');
      return;
    }
    if (IsNullOrWhiteSpace(this.directionCode)) {
      this._notifications.bulkFailure('Код направления не должен быть пустым');
      return;
    }
    if (IsNullOrWhiteSpace(this.directionType)) {
      this._notifications.bulkFailure('Тип направления должен быть указан');
      return;
    }

    const direction: EducationDirection = {} as EducationDirection;
    direction.name = this.directionName;
    direction.code = this.directionCode;
    direction.type = this.directionType;
    this._service
      .create(direction)
      .pipe(
        tap((response) => {
          this.directionCreated.emit(response);
          this.cleanInputs();
          this._notifications.bulkSuccess(
            'Добавлено новое направление подготовки',
          );
        }),
        catchError((error: HttpErrorResponse) => {
          this._handler.tryHandle(error);
          this._notifications.bulkFailure(error.error);
          return new Observable<never>();
        }),
      )
      .subscribe();
  }

  private cleanInputs(): void {
    this.directionName = '';
    this.directionCode = '';
    this.directionType = '';
  }
}
