import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ISubbmittable } from '../../../../../../../shared/models/interfaces/isubbmitable';
import { StudentGroup } from '../../../services/studentsGroup.interface';
import { EducationPlan } from '../../../../education-plans/models/education-plan-interface';
import { EducationPlanSearchService } from './education-plan-search.service';
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
export class PlanAttachmentModalComponent implements OnInit, ISubbmittable {
  @Input({ required: true }) group: StudentGroup;
  @Output() visibility: EventEmitter<boolean> = new EventEmitter<boolean>();
  @Output() returnGroup: EventEmitter<StudentGroup> =
    new EventEmitter<StudentGroup>();
  @Output() refreshEmitter: EventEmitter<void> = new EventEmitter();
  @Output() successEmitter: EventEmitter<void> = new EventEmitter();
  @Output() failureEmitter: EventEmitter<void> = new EventEmitter();

  protected plans: EducationPlan[];
  protected directions: EducationDirection[];

  protected isSemesterSelectionVisible: boolean;

  protected selectedPlan: EducationPlan | null;

  public constructor(
    private readonly _directionsFetchService: SearchDirectionsService,
    private readonly _plansFetchService: EducationPlanSearchService,
    private readonly _attachmentService: EducationPlanAttachmentService,
    protected readonly notificationService: UserOperationNotificationService,
  ) {
    this.isSemesterSelectionVisible = false;
    this.directions = [];
    this.plans = [];
    this.selectedPlan = null;
  }

  public submit(): void {
    if (this.selectedPlan) {
      this.isSemesterSelectionVisible = true;
    }
  }

  public submitDeattachment(): void {
    const handler = EducationPlanDeattachmentHandler(
      this.notificationService,
      this.successEmitter,
      this.failureEmitter,
      this.refreshEmitter,
    );

    this._attachmentService
      .deattachPlan(this.group)
      .pipe(
        tap((response) => {
          handler.handle(response);
          this.group = response;
          this.selectedPlan = null;
          this.fetchDirections();
        }),
        catchError((error: HttpErrorResponse) => handler.handleError(error)),
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
