import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ISubbmittable } from '../../../../../../shared/models/interfaces/isubbmitable';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { FacadeService } from '../../services/facade.service';
import { EducationDirection } from '../../models/education-direction-interface';
import { EducationDirectionCreationHandler } from './education-directions-create-handler';
import { catchError, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-education-direction-create-dropdown',
  templateUrl: './education-direction-create-dropdown.component.html',
  styleUrl: './education-direction-create-dropdown.component.scss',
})
export class EducationDirectionCreateDropdownComponent
  implements ISubbmittable
{
  @Input() visibility: boolean = false;
  @Output() visibilityChange: EventEmitter<boolean> = new EventEmitter();
  @Output() refreshEmitter: EventEmitter<void> = new EventEmitter();
  protected directionName: string = '';
  protected directionCode: string = '';
  protected directionType: string = '';
  protected isSelectingType = false;

  public constructor(
    private readonly _notificationService: UserOperationNotificationService,
    private readonly _facadeService: FacadeService,
  ) {}

  protected closeDropdown(): void {
    this.visibility = false;
    this.visibilityChange.emit(false);
  }

  public submit(): void {
    if (this.isDirectionNameEmpty()) return;
    if (this.isDirectionTypeEmpty()) return;
    if (this.isDirectionCodeEmpty()) return;
    const direction = this.createEducationDirection();
    const handler = EducationDirectionCreationHandler(
      this._notificationService,
      this.refreshEmitter,
    );
    this._facadeService
      .create(direction)
      .pipe(
        tap((response) => {
          handler.handle(response);
          this.cleanInputs();
        }),
        catchError((error: HttpErrorResponse) => {
          this.cleanInputs();
          return handler.handleError(error);
        }),
      )
      .subscribe();
  }

  private createEducationDirection(): EducationDirection {
    const direction = {} as EducationDirection;
    direction.name = this.directionName;
    direction.code = this.directionCode;
    direction.type = this.directionType;
    return direction;
  }

  private isDirectionNameEmpty(): boolean {
    if (this.directionName.length == 0) {
      this._notificationService.SetMessage =
        'Название направления подготовки не заполнено';
      this._notificationService.failure();
      return true;
    }
    return false;
  }

  private isDirectionCodeEmpty(): boolean {
    if (this.directionCode.length == 0) {
      this._notificationService.SetMessage =
        'Код направления подготовки не заполнен';
      this._notificationService.failure();
      return true;
    }
    return false;
  }

  private isDirectionTypeEmpty(): boolean {
    if (this.directionType.length == 0) {
      this._notificationService.SetMessage =
        'Тип направления подготовки не выбран';
      this._notificationService.failure();
      return true;
    }
    return false;
  }

  private cleanInputs(): void {
    this.directionType = '';
    this.directionName = '';
    this.directionCode = '';
  }
}
