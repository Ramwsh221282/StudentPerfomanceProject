import { Component, EventEmitter, Input, Output } from '@angular/core';
import { StudentGroup } from '../../../../services/studentsGroup.interface';
import { UserOperationNotificationService } from '../../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { EducationPlanAttachmentService } from '../../../../services/education-plan-attachment.service';
import { ISubbmittable } from '../../../../../../../../shared/models/interfaces/isubbmitable';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-deattach-education-plan-popup',
  templateUrl: './deattach-education-plan-popup.component.html',
  styleUrl: './deattach-education-plan-popup.component.scss',
})
export class DeattachEducationPlanPopupComponent implements ISubbmittable {
  @Input({ required: true }) group: StudentGroup;
  @Input({ required: true }) visibility: boolean = false;
  @Output() planDeattached: EventEmitter<StudentGroup> = new EventEmitter();
  @Output() visibilityChanged: EventEmitter<boolean> = new EventEmitter();

  public constructor(
    private readonly _notificationService: UserOperationNotificationService,
    private readonly _planAttachmentService: EducationPlanAttachmentService,
  ) {}

  public submit(): void {
    if (this.isGroupWithoutPlan()) return;
    this._planAttachmentService
      .deattachPlan(this.group)
      .pipe(
        tap((response) => {
          this.planDeattached.emit(this.group);
          this.close();
        }),
        catchError((error: HttpErrorResponse) => {
          this._notificationService.SetMessage = error.error;
          this._notificationService.failure();
          return new Observable<never>();
        }),
      )
      .subscribe();
  }

  protected close(): void {
    this.visibility = false;
    this.visibilityChanged.emit(this.visibility);
  }

  private isGroupWithoutPlan(): boolean {
    if (this.group.plan == null) {
      this._notificationService.SetMessage = 'У группы не задан учебный план';
      this._notificationService.failure();
      return true;
    }
    return false;
  }
}
