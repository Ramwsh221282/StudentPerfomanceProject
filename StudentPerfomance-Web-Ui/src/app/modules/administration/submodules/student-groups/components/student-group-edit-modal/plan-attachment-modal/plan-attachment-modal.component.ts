import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ISubbmittable } from '../../../../../../../shared/models/interfaces/isubbmitable';
import { IFailureNotificatable } from '../../../../../../../shared/models/interfaces/ifailure-notificatable';
import { ISuccessNotificatable } from '../../../../../../../shared/models/interfaces/isuccess-notificatable';
import { StudentGroup } from '../../../services/studentsGroup.interface';
import { EducationPlan } from '../../../../education-plans/models/education-plan-interface';
import { EducationPlanSearchService } from './education-plan-search.service';
import { EducationPlanAttachmentHandler } from './education-plan-attachment-handler';
import { EducationPlanAttachmentService } from './education-plan-attachment.service';
import { UserOperationNotificationService } from '../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { catchError, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { EducationPlanDeattachmentHandler } from './edicaton-plan-deattachment-handler';
import { EducationDirection } from '../../../../education-directions/models/education-direction-interface';
import { SearchDirectionsService } from '../../../../education-directions/services/search-directions.service';

@Component({
  selector: 'app-plan-attachment-modal',
  templateUrl: './plan-attachment-modal.component.html',
  styleUrl: './plan-attachment-modal.component.scss',
  providers: [EducationPlanSearchService, EducationPlanAttachmentService],
})
export class PlanAttachmentModalComponent
  implements
    OnInit,
    ISubbmittable,
    IFailureNotificatable,
    ISuccessNotificatable
{
  @Input({ required: true }) group: StudentGroup;
  @Output() visibility: EventEmitter<boolean> = new EventEmitter<boolean>();
  @Output() returnGroup: EventEmitter<StudentGroup> =
    new EventEmitter<StudentGroup>();

  protected plans: EducationPlan[];
  protected directions: EducationDirection[];

  protected isSuccess: boolean;
  protected isFailure: boolean;
  protected isSemesterSelectionVisible: boolean;

  protected selectedPlan: EducationPlan;

  public constructor(
    private readonly _directionsFetchService: SearchDirectionsService,
    private readonly _plansFetchService: EducationPlanSearchService,
    private readonly _attachmentService: EducationPlanAttachmentService,
    protected readonly notificationService: UserOperationNotificationService
  ) {
    this.isSemesterSelectionVisible = false;
    this.directions = [];
    this.plans = [];
    this.selectedPlan = {} as EducationPlan;
  }

  public notifySuccess(): void {
    this.isSuccess = true;
  }

  public manageSuccess(value: boolean): void {
    this.isSuccess = value;
  }

  public notifyFailure(): void {
    this.isFailure = true;
  }

  public manageFailure(value: boolean): void {
    this.isFailure = value;
  }

  public submit(): void {
    if (this.selectedPlan.year == undefined) {
      this.notificationService.SetMessage = 'Необходимо выбрать учебный план';
      this.notifyFailure();
      return;
    }
    this.isSemesterSelectionVisible = true;
  }

  public submitDeattachment(): void {
    const handler = EducationPlanDeattachmentHandler(
      this.notificationService,
      this,
      this
    );

    this._attachmentService
      .deattachPlan(this.group)
      .pipe(
        tap((response) => {
          handler.handle(response);
          this.group = response;
        }),
        catchError((error: HttpErrorResponse) => handler.handleError(error))
      )
      .subscribe();
  }

  public ngOnInit(): void {
    if (this.group.plan == null) {
      this.fetchDirections();
    }
  }

  protected close(): void {
    this.returnGroup.emit(this.group);
    this.visibility.emit(false);
  }

  protected selectPlan(plan: EducationPlan): void {
    this.selectedPlan = { ...plan };
    this.selectedPlan.direction = { ...plan.direction };
  }

  protected fetchPlans(direction: EducationDirection): void {
    this._plansFetchService.getByDirection(direction).subscribe((response) => {
      this.plans = response;
    });
  }

  protected fetchDirections(): void {
    this._directionsFetchService.getAll().subscribe((response) => {
      this.directions = response;
      if (this.directions.length > 0) {
        this.fetchPlans(this.directions[0]);
      }
    });
  }

  protected selectDirection(direction: any): void {
    const selectedCode: string = direction.target.value;
    const requestedDirection: EducationDirection | undefined =
      this.directions.find((d) => d.code == selectedCode);
    const selectedDirection: EducationDirection = requestedDirection!;
    this.fetchPlans(selectedDirection);
  }
}
