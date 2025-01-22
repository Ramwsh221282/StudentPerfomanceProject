import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { EducationPlan } from '../../../../modules/administration/submodules/education-plans/models/education-plan-interface';
import { EducationDirection } from '../../../../modules/administration/submodules/education-directions/models/education-direction-interface';
import { NotificationService } from '../../../../building-blocks/notifications/notification.service';
import { FloatingLabelInputComponent } from '../../../../building-blocks/floating-label-input/floating-label-input.component';
import { GreenOutlineButtonComponent } from '../../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { RedOutlineButtonComponent } from '../../../../building-blocks/buttons/red-outline-button/red-outline-button.component';
import { SelectDirectionTypeDropdownComponent } from '../../education-directions-inline-list/create-education-direction-dialog/select-direction-type-dropdown/select-direction-type-dropdown.component';
import { YellowButtonComponent } from '../../../../building-blocks/buttons/yellow-button/yellow-button.component';
import { IsNullOrWhiteSpace } from '../../../../shared/utils/string-helper';
import { EditEducationPlanService } from './edit-education-plan.service';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-edit-education-plan-dialog',
  imports: [
    FloatingLabelInputComponent,
    GreenOutlineButtonComponent,
    RedOutlineButtonComponent,
    SelectDirectionTypeDropdownComponent,
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
  ) {}

  public ngOnInit() {
    this.planCopy = { ...this.plan };
    this.yearInput = this.planCopy.year.toString();
  }

  public edit(): void {
    if (IsNullOrWhiteSpace(this.yearInput)) {
      this._notifications.setMessage('Год учебного плана должен быть указан');
      this._notifications.failure();
      this._notifications.turn();
    }
    const number = Number(this.yearInput);
    if (Number.isNaN(number)) {
      this._notifications.setMessage('Год учебного плана должен быть числом');
      this._notifications.failure();
      this._notifications.turn();
      return;
    }
    this.planCopy.year = number;
    this._service
      .edit(this.plan, this.planCopy, this.direction)
      .pipe(
        tap(() => {
          this._notifications.setMessage('Год учебного плана изменён');
          this._notifications.success();
          this._notifications.turn();
          this.plan.year = this.planCopy.year;
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

  public reset(): void {
    this.ngOnInit();
  }
}
