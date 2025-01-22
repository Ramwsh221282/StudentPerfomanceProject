import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FloatingLabelInputComponent } from '../../../../building-blocks/floating-label-input/floating-label-input.component';
import { GreenOutlineButtonComponent } from '../../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { RedOutlineButtonComponent } from '../../../../building-blocks/buttons/red-outline-button/red-outline-button.component';
import { RedButtonComponent } from '../../../../building-blocks/buttons/red-button/red-button.component';
import { EducationPlan } from '../../../../modules/administration/submodules/education-plans/models/education-plan-interface';
import { EducationDirection } from '../../../../modules/administration/submodules/education-directions/models/education-direction-interface';
import { RemoveEducationPlanService } from './remove-education-plan.service';
import { NotificationService } from '../../../../building-blocks/notifications/notification.service';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-remove-education-plan-dialog',
  imports: [
    FloatingLabelInputComponent,
    GreenOutlineButtonComponent,
    RedOutlineButtonComponent,
    RedButtonComponent,
  ],
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
  ) {}

  public delete(): void {
    this._service
      .delete(this.plan, this.direction)
      .pipe(
        tap(() => {
          this._notifications.setMessage('Учебный план был удалёен');
          this._notifications.success();
          this._notifications.turn();
          this.educationPlanRemoved.emit(this.plan);
          this.visibilityChange.emit();
        }),
        catchError((error: HttpErrorResponse) => {
          this._notifications.setMessage(error.error);
          this._notifications.failure();
          this._notifications.turn();
          this.visibilityChange.emit();
          return new Observable<never>();
        }),
      )
      .subscribe();
  }
}
