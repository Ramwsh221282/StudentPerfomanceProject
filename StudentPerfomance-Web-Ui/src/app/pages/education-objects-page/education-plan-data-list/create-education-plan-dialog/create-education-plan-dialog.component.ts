import { Component, EventEmitter, Input, Output } from '@angular/core';
import { EducationDirection } from '../../../../modules/administration/submodules/education-directions/models/education-direction-interface';
import { EducationPlan } from '../../../../modules/administration/submodules/education-plans/models/education-plan-interface';
import { NotificationService } from '../../../../building-blocks/notifications/notification.service';
import { FloatingLabelInputComponent } from '../../../../building-blocks/floating-label-input/floating-label-input.component';
import { GreenOutlineButtonComponent } from '../../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { RedOutlineButtonComponent } from '../../../../building-blocks/buttons/red-outline-button/red-outline-button.component';
import { IsNullOrWhiteSpace } from '../../../../shared/utils/string-helper';
import { CreateEducationPlanService } from './create-education-plan.service';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { UnauthorizedErrorHandler } from '../../../../shared/models/common/401-error-handler/401-error-handler.service';

@Component({
  selector: 'app-create-education-plan-dialog',
  imports: [
    FloatingLabelInputComponent,
    GreenOutlineButtonComponent,
    RedOutlineButtonComponent,
  ],
  templateUrl: './create-education-plan-dialog.component.html',
  styleUrl: './create-education-plan-dialog.component.scss',
  standalone: true,
})
export class CreateEducationPlanDialogComponent {
  @Output() visibilityChange: EventEmitter<void> = new EventEmitter<void>();
  @Output() educationPlanCreated: EventEmitter<EducationPlan> =
    new EventEmitter<EducationPlan>();
  @Input({ required: true }) direction: EducationDirection;
  public year: string = '';

  public constructor(
    private readonly _service: CreateEducationPlanService,
    private readonly _notifications: NotificationService,
    private readonly _handler: UnauthorizedErrorHandler,
  ) {}

  public create(): void {
    if (IsNullOrWhiteSpace(this.year)) {
      this._notifications.bulkSuccess('Год учебного плана не был указан');
      return;
    }
    const planYear = Number(this.year);
    if (Number.isNaN(planYear)) {
      this._notifications.bulkSuccess('Год учебного плана должен быть числом');
      return;
    }
    const plan: EducationPlan = {} as EducationPlan;
    plan.year = Number(this.year);
    plan.direction = { ...this.direction };
    this._service
      .create(plan, this.direction)
      .pipe(
        tap((response) => {
          this._notifications.bulkSuccess('Добавлен новый учебный план');
          this.cleanInputs();
          this.educationPlanCreated.emit(response);
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
    this.year = '';
  }
}
