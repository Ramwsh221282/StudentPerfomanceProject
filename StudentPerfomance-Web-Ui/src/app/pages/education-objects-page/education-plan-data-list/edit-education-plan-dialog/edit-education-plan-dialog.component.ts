import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { EducationPlan } from '../../../../modules/administration/submodules/education-plans/models/education-plan-interface';
import { EducationDirection } from '../../../../modules/administration/submodules/education-directions/models/education-direction-interface';
import { NotificationService } from '../../../../building-blocks/notifications/notification.service';
import { FloatingLabelInputComponent } from '../../../../building-blocks/floating-label-input/floating-label-input.component';
import { GreenOutlineButtonComponent } from '../../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { RedOutlineButtonComponent } from '../../../../building-blocks/buttons/red-outline-button/red-outline-button.component';
import { YellowButtonComponent } from '../../../../building-blocks/buttons/yellow-button/yellow-button.component';
import { IsNullOrWhiteSpace } from '../../../../shared/utils/string-helper';
import { EditEducationPlanService } from './edit-education-plan.service';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { UnauthorizedErrorHandler } from '../../../../shared/models/common/401-error-handler/401-error-handler.service';

@Component({
  selector: 'app-edit-education-plan-dialog',
  imports: [
    FloatingLabelInputComponent,
    GreenOutlineButtonComponent,
    RedOutlineButtonComponent,
    YellowButtonComponent,
  ],
  templateUrl: './edit-education-plan-dialog.component.html',
  styleUrl: './edit-education-plan-dialog.component.scss',
  standalone: true,
})
export class EditEducationPlanDialogComponent implements OnInit {
  @Input({ required: true }) plan: EducationPlan;
  @Input({ required: true }) direction: EducationDirection;
  @Output() visibilityChange: EventEmitter<void> = new EventEmitter();
  public planCopy: EducationPlan;
  public yearInput: string = '';

  constructor(
    private readonly _service: EditEducationPlanService,
    private readonly _notifications: NotificationService,
    private readonly _handler: UnauthorizedErrorHandler,
  ) {}

  public ngOnInit() {
    this.planCopy = { ...this.plan };
    this.yearInput = this.planCopy.year.toString();
  }

  public edit(): void {
    if (IsNullOrWhiteSpace(this.yearInput)) {
      this._notifications.bulkFailure('Год учебного плана должен быть указан');
      return;
    }
    const number = Number(this.yearInput);
    if (Number.isNaN(number)) {
      this._notifications.bulkFailure('Год учебного плана должен быть числом');
      return;
    }
    this.planCopy.year = number;
    this._service
      .edit(this.plan, this.planCopy, this.direction)
      .pipe(
        tap(() => {
          this._notifications.bulkSuccess('Год учебного плана изменён');
          this.plan.year = this.planCopy.year;
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

  public reset(): void {
    this.ngOnInit();
  }
}
