import { Component, EventEmitter, Input, Output } from '@angular/core';
import { EducationDirection } from '../../../../modules/administration/submodules/education-directions/models/education-direction-interface';
import { EducationPlan } from '../../../../modules/administration/submodules/education-plans/models/education-plan-interface';
import { NotificationService } from '../../../../building-blocks/notifications/notification.service';
import { FloatingLabelInputComponent } from '../../../../building-blocks/floating-label-input/floating-label-input.component';
import { GreenOutlineButtonComponent } from '../../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { RedOutlineButtonComponent } from '../../../../building-blocks/buttons/red-outline-button/red-outline-button.component';
import { SelectDirectionTypeDropdownComponent } from '../../education-directions-inline-list/create-education-direction-dialog/select-direction-type-dropdown/select-direction-type-dropdown.component';
import { IsNullOrWhiteSpace } from '../../../../shared/utils/string-helper';
import { CreateEducationPlanService } from './create-education-plan.service';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-create-education-plan-dialog',
  imports: [
    FloatingLabelInputComponent,
    GreenOutlineButtonComponent,
    RedOutlineButtonComponent,
    SelectDirectionTypeDropdownComponent,
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
  ) {}

  public create(): void {
    if (IsNullOrWhiteSpace(this.year)) {
      this._notifications.setMessage('Год учебного плана не был указан');
      this._notifications.failure();
      this._notifications.turn();
      return;
    }
    const planYear = Number(this.year);
    if (Number.isNaN(planYear)) {
      this._notifications.setMessage('Год учебного плана должен быть числом');
      this._notifications.failure();
      this._notifications.turn();
      return;
    }
    const plan: EducationPlan = {} as EducationPlan;
    plan.year = Number(this.year);
    plan.direction = { ...this.direction };
    this._service
      .create(plan, this.direction)
      .pipe(
        tap((response) => {
          this._notifications.setMessage('Добавлен новый учебный план');
          this._notifications.success();
          this._notifications.turn();
          this.cleanInputs();
          this.educationPlanCreated.emit(response);
        }),
        catchError((error: HttpErrorResponse) => {
          this._notifications.setMessage(error.error);
          this._notifications.failure();
          this._notifications.turn();
          return new Observable<never>();
        }),
      )
      .subscribe();
  }

  private cleanInputs(): void {
    this.year = '';
  }
}
