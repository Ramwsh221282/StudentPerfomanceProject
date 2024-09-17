import { Component, OnInit } from '@angular/core';
import { DepartmentFormBase } from '../../models/base-department-form';
import { DepartmentsFacadeService } from '../../services/departments-facade.service';
import { DepartmentDeletionHandler } from './department-deletion-handler';
import { HttpErrorResponse } from '@angular/common/http';
import { catchError, tap } from 'rxjs';
import { DepartmentUpdateHandler } from './department-update-handler';
import { INotificatable } from '../../../../../../shared/models/inotificated-component-interface/inotificatable.interface';
import { ModalState } from '../../../../../../shared/models/modals/modal-state';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';

@Component({
  selector: 'app-manage-department',
  templateUrl: './manage-department.component.html',
  styleUrl: './manage-department.component.scss',
})
export class ManageDepartmentComponent
  extends DepartmentFormBase
  implements OnInit, INotificatable
{
  public readonly successModalState: ModalState = new ModalState();
  public readonly failureModalState: ModalState = new ModalState();
  public constructor(
    protected readonly facadeService: DepartmentsFacadeService,
    private readonly _notificationService: UserOperationNotificationService
  ) {
    super('Редактирование кафедры');
  }

  public ngOnInit(): void {
    this.initForm();
  }

  protected override submit(): void {
    this.ngOnInit();
  }

  protected deleteClick(): void {
    const handler = DepartmentDeletionHandler(
      this.facadeService,
      this,
      this._notificationService
    );
    this.facadeService
      .delete(this.createDepartmentFromForm())
      .pipe(
        tap((response) => handler.handle(response)),
        catchError((error: HttpErrorResponse) => handler.handleError(error))
      )
      .subscribe();
    this.submit();
  }

  protected editClick(): void {
    const handler = DepartmentUpdateHandler(
      this.facadeService,
      this,
      this._notificationService
    );
    this.facadeService
      .update(this.createDepartmentFromForm())
      .pipe(
        tap((response) => handler.handle(response)),
        catchError((error: HttpErrorResponse) => handler.handleError(error))
      )
      .subscribe();
    this.submit();
  }
}
