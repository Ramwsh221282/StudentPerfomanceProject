import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { tap, catchError } from 'rxjs';
import { ModalState } from '../../../../../../shared/models/modals/modal-state';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { BaseTeacherForm } from '../../models/base-teacher-form';
import { FacadeTeacherService } from '../../services/facade-teacher.service';
import { TeacherDeletedHandler } from './teacher-deleted-handler';
import { TeacherUpdatedHandler } from './teacher-updated-handler';
import { INotificatable } from '../../../../../../shared/models/inotificated-component-interface/inotificatable.interface';

@Component({
  selector: 'app-teacher-manage',
  templateUrl: './teacher-manage.component.html',
  styleUrl: './teacher-manage.component.scss',
})
export class TeacherManageComponent
  extends BaseTeacherForm
  implements OnInit, INotificatable
{
  public readonly successModalState: ModalState = new ModalState();
  public readonly failureModalState: ModalState = new ModalState();
  public constructor(
    protected readonly facadeService: FacadeTeacherService,
    private readonly _notificationService: UserOperationNotificationService
  ) {
    super('Редактирование преподавателя');
  }

  public ngOnInit(): void {
    this.initForm();
  }

  protected override submit(): void {
    this.ngOnInit();
  }

  protected deleteClick(): void {
    const handler = TeacherDeletedHandler(
      this.facadeService,
      this,
      this._notificationService
    );
    this.facadeService
      .delete(this.createTeacherFromForm(this.facadeService.currentDepartment))
      .pipe(
        tap((response) => handler.handle(response)),
        catchError((error: HttpErrorResponse) => handler.handleError(error))
      )
      .subscribe();
    this.submit();
  }

  protected editClick(): void {
    const handler = TeacherUpdatedHandler(
      this.facadeService,
      this,
      this._notificationService
    );
    this.facadeService
      .update(this.createTeacherFromForm(this.facadeService.currentDepartment))
      .pipe(
        tap((response) => handler.handle(response)),
        catchError((error: HttpErrorResponse) => handler.handleError(error))
      )
      .subscribe();
    this.submit();
  }
}
