import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { EducationPlanBaseForm } from '../../models/education-plan-base-form';
import { SearchDirectionsService } from '../../../education-directions/services/search-directions.service';
import { FacadeService } from '../../services/facade.service';
import { EducationDirection } from '../../../education-directions/models/education-direction-interface';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { ISubbmittable } from '../../../../../../shared/models/interfaces/isubbmitable';
import { EducationPlanCreationHandler } from './education-plan-creation-handler';
import { catchError, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-education-plan-creation-modal',
  templateUrl: './education-plan-creation-modal.component.html',
  styleUrl: './education-plan-creation-modal.component.scss',
  providers: [SearchDirectionsService],
})
export class EducationPlanCreationModalComponent
  extends EducationPlanBaseForm
  implements OnInit, ISubbmittable
{
  @Output() visibility: EventEmitter<boolean> = new EventEmitter();
  @Output() refreshEmitter: EventEmitter<void> = new EventEmitter();
  @Output() successEmitter: EventEmitter<void> = new EventEmitter();
  @Output() failureEmitter: EventEmitter<void> = new EventEmitter();

  protected directions: EducationDirection[];
  protected direction: EducationDirection | null;

  public constructor(
    private readonly _facadeService: FacadeService,
    private readonly _searchService: SearchDirectionsService,
    protected readonly notificationService: UserOperationNotificationService,
  ) {
    super();
    this.direction = null;
  }

  public override submit(): void {
    if (this.direction) {
      const plan = this.createEducationPlanFromForm();
      plan.direction = { ...this.direction };
      const handler = EducationPlanCreationHandler(
        this.notificationService,
        this.successEmitter,
        this.failureEmitter,
        this.refreshEmitter,
        this.visibility,
      );
      this._facadeService
        .create(plan)
        .pipe(
          tap((response) => handler.handle(response)),
          catchError((error: HttpErrorResponse) => handler.handleError(error)),
        )
        .subscribe();
    }
  }

  public ngOnInit(): void {
    this.getAllDirections();
    this.initForm();
  }

  public getAllDirections(): void {
    this._searchService.getAll().subscribe((response) => {
      this.directions = response;
    });
  }

  public selectDirection(direction: EducationDirection): void {
    this.direction = { ...direction };
  }
}
