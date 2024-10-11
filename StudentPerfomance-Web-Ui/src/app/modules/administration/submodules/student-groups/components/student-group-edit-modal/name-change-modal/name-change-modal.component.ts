import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ISubbmittable } from '../../../../../../../shared/models/interfaces/isubbmitable';
import { ISuccessNotificatable } from '../../../../../../../shared/models/interfaces/isuccess-notificatable';
import { IFailureNotificatable } from '../../../../../../../shared/models/interfaces/ifailure-notificatable';
import { StudentGroup } from '../../../services/studentsGroup.interface';
import { StudentGroupsFacadeService } from '../../../services/student-groups-facade.service';
import { UserOperationNotificationService } from '../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { GroupNameChangeHandler } from './group-change-name-handler';
import { catchError, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { EducationPlan } from '../../../../education-plans/models/education-plan-interface';
import { EducationDirection } from '../../../../education-directions/models/education-direction-interface';

@Component({
  selector: 'app-name-change-modal',
  templateUrl: './name-change-modal.component.html',
  styleUrl: './name-change-modal.component.scss',
  providers: [UserOperationNotificationService],
})
export class NameChangeModalComponent
  implements
    ISubbmittable,
    ISuccessNotificatable,
    IFailureNotificatable,
    OnInit
{
  @Input({ required: true }) group: StudentGroup;
  @Output() visibility: EventEmitter<boolean> = new EventEmitter<boolean>();
  @Output() returnGroup: EventEmitter<StudentGroup> =
    new EventEmitter<StudentGroup>();

  protected editableGroup: StudentGroup;
  protected copy: StudentGroup;

  protected isSuccess: boolean;
  protected isFailure: boolean;

  public constructor(
    private readonly _facadeService: StudentGroupsFacadeService,
    protected readonly notificationService: UserOperationNotificationService
  ) {
    this.isSuccess = false;
    this.isFailure = false;
  }

  public ngOnInit(): void {
    this.editableGroup = { ...this.group };
    this.copy = { ...this.group };
  }

  public notifyFailure(): void {
    this.isFailure = true;
  }

  public manageFailure(value: boolean): void {
    this.isFailure = value;
  }

  public notifySuccess(): void {
    this.isSuccess = true;
  }

  public manageSuccess(value: boolean): void {
    this.isSuccess = value;
  }

  public submit(): void {
    const handler = GroupNameChangeHandler(
      this.notificationService,
      this,
      this,
      this.copy
    );

    this.copy.plan = {} as EducationPlan;
    this.copy.plan.direction = {} as EducationDirection;
    this.editableGroup.plan = {} as EducationPlan;
    this.editableGroup.plan.direction = {} as EducationDirection;

    this._facadeService
      .update(this.copy, this.editableGroup)
      .pipe(
        tap((response) => {
          handler.handle(response);
          this.group = response;
          this.group.plan = response.plan;
          this.group.plan.direction = response.plan.direction;
        }),
        catchError((error: HttpErrorResponse) => handler.handleError(error))
      )
      .subscribe();
    this.ngOnInit();
  }

  public close(): void {
    this.returnGroup.emit(this.group);
    this.visibility.emit(false);
  }
}
