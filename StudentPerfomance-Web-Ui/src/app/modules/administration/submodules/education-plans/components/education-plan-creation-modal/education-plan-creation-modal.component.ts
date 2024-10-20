import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { EducationPlanBaseForm } from '../../models/education-plan-base-form';
import { SearchDirectionsService } from '../../../education-directions/services/search-directions.service';
import { FacadeService } from '../../services/facade.service';
import { EducationDirection } from '../../../education-directions/models/education-direction-interface';
import { EducationDirectionBuilder } from '../../../education-directions/models/builders/education-direction-builder';
import { ISuccessNotificatable } from '../../../../../../shared/models/interfaces/isuccess-notificatable';
import { IFailureNotificatable } from '../../../../../../shared/models/interfaces/ifailure-notificatable';
import { EducationPlanCreationHandler } from './education-plan-creation-handler';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { catchError, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-education-plan-creation-modal',
  templateUrl: './education-plan-creation-modal.component.html',
  styleUrl: './education-plan-creation-modal.component.scss',
  providers: [SearchDirectionsService, UserOperationNotificationService],
})
export class EducationPlanCreationModalComponent
  extends EducationPlanBaseForm
  implements OnInit, ISuccessNotificatable, IFailureNotificatable
{
  @Output() visibility: EventEmitter<boolean> = new EventEmitter();

  protected isSuccess: boolean;
  protected isFailure: boolean;
  protected directions: EducationDirection[];
  protected direction: EducationDirection;

  public constructor(
    private readonly _facadeService: FacadeService,
    private readonly _searchService: SearchDirectionsService,
    protected readonly notificationService: UserOperationNotificationService
  ) {
    super();
    this.isSuccess = false;
    this.isFailure = false;
  }

  public notifyFailure(): void {
    this.isFailure = true;
  }

  protected manageFailure(value: boolean): void {
    this.isFailure = value;
  }

  public notifySuccess(): void {
    this.isSuccess = true;
  }

  protected manageSuccess(value: boolean): void {
    this.isSuccess = value;
  }

  public override submit(): void {
    const plan = this.createEducationPlanFromForm();
    plan.direction = { ...this.direction };
    const handler = EducationPlanCreationHandler(
      this._facadeService,
      this.notificationService,
      this,
      this
    );
    this._facadeService
      .create(plan)
      .pipe(
        tap((response) => handler.handle(response)),
        catchError((error: HttpErrorResponse) => handler.handleError(error))
      )
      .subscribe();
  }

  public ngOnInit(): void {
    this.getAllDirections();
    this.initForm();
    const builder: EducationDirectionBuilder = new EducationDirectionBuilder();
    this.direction = builder.buildDefault();
  }

  public searchDirections(): void {
    this._searchService.search(this.direction).subscribe((response) => {
      this.directions = response;
    });
  }

  public getAllDirections(): void {
    this._searchService.getAll().subscribe((response) => {
      this.directions = response;
    });
  }

  public close(): void {
    this.ngOnInit();
    this.visibility.emit(false);
  }

  public selectDirection(direction: EducationDirection): void {
    this.direction = { ...direction };
  }
}
