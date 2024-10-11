import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ISubbmittable } from '../../../../../../../shared/models/interfaces/isubbmitable';
import { IFailureNotificatable } from '../../../../../../../shared/models/interfaces/ifailure-notificatable';
import { ISuccessNotificatable } from '../../../../../../../shared/models/interfaces/isuccess-notificatable';
import { StudentGroup } from '../../../services/studentsGroup.interface';
import { EducationPlan } from '../../../../education-plans/models/education-plan-interface';
import { EducationPlanSearchService } from './education-plan-search.service';
import { EducationPlanBuilder } from '../../../../education-plans/models/builders/education-plan-builder';
import { EducationPlanAttachmentHandler } from './education-plan-attachment-handler';
import { EducationPlanAttachmentService } from './education-plan-attachment.service';
import { UserOperationNotificationService } from '../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { catchError, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { EducationPlanDeattachmentHandler } from './edicaton-plan-deattachment-handler';

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
  protected activePlan: EducationPlan;
  protected filterPlan: EducationPlan;
  protected isSuccess: boolean;
  protected isFailure: boolean;

  public constructor(
    private readonly _searchService: EducationPlanSearchService,
    private readonly _attachmentService: EducationPlanAttachmentService,
    protected readonly notificationService: UserOperationNotificationService
  ) {
    this.plans = [];
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
    const handler = EducationPlanAttachmentHandler(
      this.notificationService,
      this,
      this
    );

    this._attachmentService
      .attachPlan(this.group, this.activePlan)
      .pipe(
        tap((response) => {
          handler.handle(response);
          this.activePlan = response.plan;
          this.activePlan.direction = response.plan.direction;
          this.group = response;
        }),
        catchError((error: HttpErrorResponse) => handler.handleError(error))
      )
      .subscribe();
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
          this.activePlan = response.plan;
          this.activePlan.direction = response.plan.direction;
          this.group = response;
        }),
        catchError((error: HttpErrorResponse) => handler.handleError(error))
      )
      .subscribe();
  }

  public ngOnInit(): void {
    const builder: EducationPlanBuilder = new EducationPlanBuilder();
    if (this.group.plan.year == 0) this.activePlan = builder.buildDefault();
    else this.activePlan = this.group.plan;
    this.filterPlan = builder.buildDefault();
    this.fetchPlans();
  }

  protected close(): void {
    this.returnGroup.emit(this.group);
    this.visibility.emit(false);
  }

  protected selectPlan(plan: EducationPlan): void {
    this.activePlan = plan;
  }

  protected fetchPlans(): void {
    this._searchService.getAll().subscribe((response) => {
      this.plans = response;
    });
  }

  protected searchPlans(): void {
    this._searchService.search(this.filterPlan).subscribe((response) => {
      this.plans = response;
    });
  }
}
