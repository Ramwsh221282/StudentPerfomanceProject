import { Component, OnInit } from '@angular/core';
import { BaseTeacherForm } from '../../models/base-teacher-form';
import { FacadeTeacherService } from '../../services/facade-teacher.service';
import { TeacherAddedHandler } from './add-teacher-handler';
import { HttpErrorResponse } from '@angular/common/http';
import { catchError, tap } from 'rxjs';
import { INotificatable } from '../../../../../../shared/models/inotificated-component-interface/inotificatable.interface';
import { ModalState } from '../../../../../../shared/models/modals/modal-state';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';

@Component({
  selector: 'app-teacher-create',
  templateUrl: './teacher-create.component.html',
  styleUrl: './teacher-create.component.scss',
})
export class TeacherCreateComponent
  extends BaseTeacherForm
  implements OnInit, INotificatable
{
  public readonly successModalState: ModalState = new ModalState();
  public readonly failureModalState: ModalState = new ModalState();
  public constructor(
    private readonly _facadeService: FacadeTeacherService,
    private readonly _notificationService: UserOperationNotificationService
  ) {
    super('Создание преподавателя');
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
