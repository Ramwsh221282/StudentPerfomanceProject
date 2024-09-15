import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { BaseSemesterForm } from '../../models/semester-form-base';
import { INotificatable } from '../../../../shared-models/interfaces/inotificated-component-interface/inotificatable.interface';
import { SemesterFacadeService } from '../../services/semester-facade.service';
import { UserOperationNotificationService } from '../../../../shared-services/user-notifications/user-operation-notification-service.service';
import { SemesterDeletedHandler } from './semester-deleted-handler';
import { catchError, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { SuccessNotificationFormComponent } from '../../../../notification-modal-forms/success-notification-form/success-notification-form.component';
import { FailureNotificationFormComponent } from '../../../../notification-modal-forms/failure-notification-form/failure-notification-form.component';
import { NgIf } from '@angular/common';
import { ModalState } from '../../../../shared-models/models/modals/modal-state';

@Component({
  selector: 'app-manage-form',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    FormsModule,
    SuccessNotificationFormComponent,
    FailureNotificationFormComponent,
    NgIf,
  ],
  templateUrl: './manage-form.component.html',
  styleUrl: './manage-form.component.scss',
})
export class ManageFormComponent
  extends BaseSemesterForm
  implements OnInit, INotificatable
{
  public readonly successModalState: ModalState = new ModalState();
  public readonly failureModalState: ModalState = new ModalState();
  public constructor(
    protected readonly facadeService: SemesterFacadeService,
    private readonly _notificationService: UserOperationNotificationService
  ) {
    super('Редактирование семестра');
  }

  public ngOnInit(): void {
    this.initForm();
  }

  protected override submit(): void {
    this.ngOnInit();
  }

  protected deleteClick(): void {
    const handler = SemesterDeletedHandler(
      this.facadeService,
      this,
      this._notificationService
    );
    this.facadeService
      .delete(this.createSemesterFromForm())
      .pipe(
        tap((response) => handler.handle(response)),
        catchError((error: HttpErrorResponse) => handler.handleError(error))
      )
      .subscribe();
    this.submit();
  }
}
