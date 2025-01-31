import { Component, EventEmitter, Input, Output } from '@angular/core';
import { StudentGroup } from '../../../../modules/administration/submodules/student-groups/services/studentsGroup.interface';
import { DetachPlanGroupService } from './detach-plan-group.service';
import { UnauthorizedErrorHandler } from '../../../../shared/models/common/401-error-handler/401-error-handler.service';
import { StudentPageViewModel } from '../../student-page-viewmodel.service';
import { NotificationService } from '../../../../building-blocks/notifications/notification.service';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { RedButtonComponent } from '../../../../building-blocks/buttons/red-button/red-button.component';
import { RedOutlineButtonComponent } from '../../../../building-blocks/buttons/red-outline-button/red-outline-button.component';

@Component({
  selector: 'app-detach-plan-group-dialog',
  imports: [RedButtonComponent, RedOutlineButtonComponent],
  templateUrl: './detach-plan-group-dialog.component.html',
  styleUrl: './detach-plan-group-dialog.component.scss',
  standalone: true,
})
export class DetachPlanGroupDialogComponent {
  @Input({ required: true }) group: StudentGroup;
  @Output() visibilityChange: EventEmitter<void> = new EventEmitter();

  public constructor(
    private readonly _service: DetachPlanGroupService,
    private readonly _viewModel: StudentPageViewModel,
    private readonly _handler: UnauthorizedErrorHandler,
    private readonly _notifications: NotificationService,
  ) {}

  public detach(): void {
    this._service
      .detach(this.group)
      .pipe(
        tap(() => {
          this._notifications.bulkSuccess('У группы откреплён учебный план');
          this._viewModel.detachEducationPlan(this.group);
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
