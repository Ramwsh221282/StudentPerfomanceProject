import { Component, Input, OnInit } from '@angular/core';
import { SemesterPlanBaseForm } from '../models/semester-plan-base.form';
import { ReactiveFormsModule } from '@angular/forms';
import { Semester } from '../../models/semester.interface';
import { SemesterPlanFacadeService } from '../services/semester-plan-facade.service';
import { UserOperationNotificationService } from '../../../../shared-services/user-notifications/user-operation-notification-service.service';
import { INotificatable } from '../../../../shared-models/interfaces/inotificated-component-interface/inotificatable.interface';
import { ModalState } from '../../../../shared-models/models/modals/modal-state';
import { SemesterPlanCreationHandler } from './semester-plan-creation-handler';
import { catchError, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { NgIf } from '@angular/common';
import { SuccessNotificationFormComponent } from '../../../../notification-modal-forms/success-notification-form/success-notification-form.component';
import { FailureNotificationFormComponent } from '../../../../notification-modal-forms/failure-notification-form/failure-notification-form.component';

@Component({
  selector: 'app-semester-plan-create-form',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    NgIf,
    SuccessNotificationFormComponent,
    FailureNotificationFormComponent,
  ],
  templateUrl: './semester-plan-create-form.component.html',
  styleUrl: './semester-plan-create-form.component.scss',
})
export class SemesterPlanCreateFormComponent
  extends SemesterPlanBaseForm
  implements OnInit, INotificatable
{
  @Input({ required: true }) public currentSemester: Semester;
  public readonly successModalState: ModalState = new ModalState();
  public readonly failureModalState: ModalState = new ModalState();
  public constructor(
    private readonly _facadeService: SemesterPlanFacadeService,
    private readonly _notificationService: UserOperationNotificationService
  ) {
    super('Добавление дисциплины');
  }

  public ngOnInit(): void {
    this.initForm();
  }

  protected override submit(): void {
    const handler = SemesterPlanCreationHandler(
      this._facadeService,
      this._notificationService,
      this
    );
    this._facadeService
      .create(this.createSemesterPlanFromForm(this.currentSemester))
      .pipe(
        tap((response) => handler.handle(response)),
        catchError((error: HttpErrorResponse) => handler.handleError(error))
      )
      .subscribe();
    this.ngOnInit();
  }
}
