import { Component, OnInit } from '@angular/core';
import { BaseTeacherForm } from '../models/base-teacher-form';
import { ReactiveFormsModule } from '@angular/forms';
import { NgIf } from '@angular/common';
import { FacadeTeacherService } from '../services/facade-teacher.service';
import { UserOperationNotificationService } from '../../../../shared-services/user-notifications/user-operation-notification-service.service';
import { INotificatable } from '../../../../shared-models/interfaces/inotificated-component-interface/inotificatable.interface';
import { TeacherAddedHandler } from './add-teacher-handler';
import { catchError, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { SuccessNotificationFormComponent } from '../../../../notification-modal-forms/success-notification-form/success-notification-form.component';
import { FailureNotificationFormComponent } from '../../../../notification-modal-forms/failure-notification-form/failure-notification-form.component';
import { ModalState } from '../../../../shared-models/models/modals/modal-state';

@Component({
  selector: 'app-add-teacher-form',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    NgIf,
    SuccessNotificationFormComponent,
    FailureNotificationFormComponent,
  ],
  templateUrl: './add-teacher-form.component.html',
  styleUrl: './add-teacher-form.component.scss',
})
export class AddTeacherFormComponent
  extends BaseTeacherForm
  implements OnInit, INotificatable
{
  public readonly successModalState: ModalState = new ModalState();
  public readonly failureModalState: ModalState = new ModalState();
  public constructor(
    private readonly _facadeService: FacadeTeacherService,
    private readonly _notificationService: UserOperationNotificationService
  ) {
    super('Добавление преподавателя');
  }

  public ngOnInit(): void {
    this.initForm();
  }

  protected override submit(): void {
    const handler = TeacherAddedHandler(
      this._facadeService,
      this,
      this._notificationService
    );
    this._facadeService
      .create(this.createTeacherFromForm(this._facadeService.currentDepartment))
      .pipe(
        tap((response) => handler.handle(response)),
        catchError((error: HttpErrorResponse) => handler.handleError(error))
      )
      .subscribe();
    this.ngOnInit();
  }
}
