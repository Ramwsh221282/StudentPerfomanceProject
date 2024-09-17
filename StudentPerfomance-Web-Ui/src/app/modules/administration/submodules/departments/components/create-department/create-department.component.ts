import { Component, OnInit } from '@angular/core';
import { DepartmentFormBase } from '../../models/base-department-form';
import { DepartmentsFacadeService } from '../../services/departments-facade.service';
import { DepartmentCreationHandler } from './department-creation-handler';
import { HttpErrorResponse } from '@angular/common/http';
import { catchError, tap } from 'rxjs';
import { INotificatable } from '../../../../../../shared/models/inotificated-component-interface/inotificatable.interface';
import { ModalState } from '../../../../../../shared/models/modals/modal-state';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';

@Component({
  selector: 'app-create-department',
  templateUrl: './create-department.component.html',
  styleUrl: './create-department.component.scss',
})
export class CreateDepartmentComponent
  extends DepartmentFormBase
  implements OnInit, INotificatable
{
  public readonly successModalState: ModalState = new ModalState();
  public readonly failureModalState: ModalState = new ModalState();
  public constructor(
    private readonly _facadeService: DepartmentsFacadeService,
    private readonly _notificationService: UserOperationNotificationService
  ) {
    super('Создание кафедры');
  }

  public ngOnInit(): void {
    this.initForm();
  }

  protected override submit(): void {
    const handler = DepartmentCreationHandler(
      this._facadeService,
      this,
      this._notificationService
    );
    this._facadeService
      .create(this.createDepartmentFromForm())
      .pipe(
        tap((response) => handler.handle(response)),
        catchError((error: HttpErrorResponse) => handler.handleError(error))
      )
      .subscribe();
    this.ngOnInit();
  }
}
