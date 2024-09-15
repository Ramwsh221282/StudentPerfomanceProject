import { Component } from '@angular/core';
import { BaseStudentForm } from '../models/base-student-form';
import { ReactiveFormsModule } from '@angular/forms';
import { INotificatable } from '../../../../shared-models/interfaces/inotificated-component-interface/inotificatable.interface';
import { catchError, tap } from 'rxjs';
import { SuccessNotificationFormComponent } from '../../../../notification-modal-forms/success-notification-form/success-notification-form.component';
import { FailureNotificationFormComponent } from '../../../../notification-modal-forms/failure-notification-form/failure-notification-form.component';
import { NgIf } from '@angular/common';
import { UserOperationNotificationService } from '../../../../shared-services/user-notifications/user-operation-notification-service.service';
import { HttpErrorResponse } from '@angular/common/http';
import { StudentCreationHandler } from './student-creation-handler';
import { FacadeStudentService } from '../services/facade-student.service';
import { ModalState } from '../../../../shared-models/models/modals/modal-state';

@Component({
  selector: 'app-add-student-form',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    SuccessNotificationFormComponent,
    FailureNotificationFormComponent,
    NgIf,
  ],
  templateUrl: './add-student-form.component.html',
  styleUrl: './add-student-form.component.scss',
})
export class AddStudentFormComponent
  extends BaseStudentForm
  implements INotificatable
{
  public readonly successModalState: ModalState = new ModalState();
  public readonly failureModalState: ModalState = new ModalState();

  public constructor(
    private readonly _facadeService: FacadeStudentService,
    private readonly _notificationService: UserOperationNotificationService
  ) {
    super('Добавление студента');
  }

  protected override submit(): void {
    const handler = StudentCreationHandler(
      this._facadeService,
      this,
      this._notificationService
    );
    this._facadeService
      .create(this.createStudentFromForm(this._facadeService.currentGroup))
      .pipe(
        tap((response) => handler.handle(response)),
        catchError((error: HttpErrorResponse) => handler.handleError(error))
      )
      .subscribe();
    this.initForm();
  }
}
