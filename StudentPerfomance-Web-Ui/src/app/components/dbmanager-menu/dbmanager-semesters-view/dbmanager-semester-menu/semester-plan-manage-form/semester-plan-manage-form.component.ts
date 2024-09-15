import { Component, Input, OnInit } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { SemesterPlanBaseForm } from '../models/semester-plan-base.form';
import { Semester } from '../../models/semester.interface';
import { INotificatable } from '../../../../shared-models/interfaces/inotificated-component-interface/inotificatable.interface';
import { SemesterPlanFacadeService } from '../services/semester-plan-facade.service';
import { UserOperationNotificationService } from '../../../../shared-services/user-notifications/user-operation-notification-service.service';
import { NgIf } from '@angular/common';
import { ModalState } from '../../../../shared-models/models/modals/modal-state';
import { SemesterPlanDeletionHandler } from './semester-plan-deletion-handler';
import { catchError, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { SuccessNotificationFormComponent } from '../../../../notification-modal-forms/success-notification-form/success-notification-form.component';
import { FailureNotificationFormComponent } from '../../../../notification-modal-forms/failure-notification-form/failure-notification-form.component';

@Component({
  selector: 'app-semester-plan-manage-form',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    NgIf,
    SuccessNotificationFormComponent,
    FailureNotificationFormComponent,
  ],
  templateUrl: './semester-plan-manage-form.component.html',
  styleUrl: './semester-plan-manage-form.component.scss',
})
export class SemesterPlanManageFormComponent
  extends SemesterPlanBaseForm
  implements OnInit, INotificatable
{
  @Input({ required: true }) public currentSemester: Semester;
  public readonly successModalState: ModalState = new ModalState();
  public readonly failureModalState: ModalState = new ModalState();
  public constructor(
    protected readonly facadeService: SemesterPlanFacadeService,
    private readonly _notificationService: UserOperationNotificationService
  ) {
    super('Управление дисциплиной');
  }

  public ngOnInit(): void {
    this.initForm();
  }

  protected override submit(): void {
    const handler = SemesterPlanDeletionHandler(
      this.facadeService,
      this._notificationService,
      this
    );
    this.facadeService
      .delete(this.createSemesterPlanFromForm(this.currentSemester))
      .pipe(
        tap((response) => handler.handle(response)),
        catchError((error: HttpErrorResponse) => handler.handleError(error))
      )
      .subscribe();
    this.ngOnInit();
  }
}
