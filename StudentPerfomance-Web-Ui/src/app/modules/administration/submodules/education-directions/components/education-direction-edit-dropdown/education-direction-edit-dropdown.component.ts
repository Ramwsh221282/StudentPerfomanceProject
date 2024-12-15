import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { EducationDirection } from '../../models/education-direction-interface';
import { ISubbmittable } from '../../../../../../shared/models/interfaces/isubbmitable';
import { FacadeService } from '../../services/facade.service';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { catchError, Observable, tap } from 'rxjs';
import { EducationDirectionEditNotificationBulder } from './education-direction-edit-notification';

@Component({
  selector: 'app-education-direction-edit-dropdown',
  templateUrl: './education-direction-edit-dropdown.component.html',
  styleUrl: './education-direction-edit-dropdown.component.scss',
})
export class EducationDirectionEditDropdownComponent
  implements OnInit, ISubbmittable
{
  @Input({ required: true }) public direction: EducationDirection;
  @Input({ required: true }) visibility: boolean = false;
  @Output() visibilityChanged: EventEmitter<boolean> = new EventEmitter();
  protected directionCopy: EducationDirection;
  protected isSelectingType: boolean = false;

  public constructor(
    private readonly _facadeService: FacadeService,
    private readonly _notificationService: UserOperationNotificationService,
  ) {}

  public submit(): void {
    this._facadeService
      .update(this.directionCopy, this.direction)
      .pipe(
        tap((response) => {
          this.direction = { ...response };
          this._notificationService.SetMessage =
            new EducationDirectionEditNotificationBulder(
              this.directionCopy,
            ).buildMessage(response);
          this._notificationService.success();
        }),
        catchError((error) => {
          this._notificationService.SetMessage = error.error;
          this._notificationService.failure();
          this.direction = { ...this.directionCopy };
          return new Observable<never>();
        }),
      )
      .subscribe();
  }

  public ngOnInit(): void {
    this.directionCopy = { ...this.direction };
  }

  protected closeDropdown(): void {
    this.visibility = false;
    this.visibilityChanged.emit(this.visibility);
  }
}
