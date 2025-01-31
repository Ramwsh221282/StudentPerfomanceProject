import { Component, EventEmitter, Input, Output } from '@angular/core';
import { RedOutlineButtonComponent } from '../../../../building-blocks/buttons/red-outline-button/red-outline-button.component';
import { RedButtonComponent } from '../../../../building-blocks/buttons/red-button/red-button.component';
import { EducationPlan } from '../../../../modules/administration/submodules/education-plans/models/education-plan-interface';
import { EducationDirection } from '../../../../modules/administration/submodules/education-directions/models/education-direction-interface';
import { RemoveEducationPlanService } from './remove-education-plan.service';
import { NotificationService } from '../../../../building-blocks/notifications/notification.service';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { UnauthorizedErrorHandler } from '../../../../shared/models/common/401-error-handler/401-error-handler.service';

@Component({
  selector: 'app-remove-education-plan-dialog',
  imports: [RedOutlineButtonComponent, RedButtonComponent],
  templateUrl: './remove-education-plan-dialog.component.html',
  styleUrl: './remove-education-plan-dialog.component.scss',
  standalone: true,
})
export class RemoveEducationPlanDialogComponent {
  @Output() visibilityChange: EventEmitter<void> = new EventEmitter<void>();
  @Output() educationPlanRemoved: EventEmitter<EducationPlan> =
    new EventEmitter();
  @Input({ required: true }) plan: EducationPlan;
  @Input({ required: true }) direction: EducationDirection;

  public constructor(
    private readonly _service: RemoveEducationPlanService,
    private readonly _notifications: NotificationService,
    private readonly _handler: UnauthorizedErrorHandler,
  ) {}

  public delete(): void {
    this._service
      .delete(this.plan, this.direction)
      .pipe(
        tap(() => {
          this._notifications.bulkSuccess('Учебный план был удалёен');
          this.educationPlanRemoved.emit(this.plan);
          this.visibilityChange.emit();
        }),
        catchError((error: HttpErrorResponse) => {
          this._handler.tryHandle(error);
          this._notifications.bulkFailure(error.error);
          this.visibilityChange.emit();
          return new Observable<never>();
        }),
      )
      .subscribe();
  }
}
