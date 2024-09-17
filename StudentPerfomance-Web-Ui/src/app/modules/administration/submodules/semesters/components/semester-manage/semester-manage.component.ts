import { Component, OnInit } from '@angular/core';
import { BaseSemesterForm } from '../../models/semester-form-base';
import { SemesterFacadeService } from '../../services/semester-facade.service';
import { SemesterDeletedHandler } from './semester-deleted-handler';
import { HttpErrorResponse } from '@angular/common/http';
import { catchError, tap } from 'rxjs';
import { INotificatable } from '../../../../../../shared/models/inotificated-component-interface/inotificatable.interface';
import { ModalState } from '../../../../../../shared/models/modals/modal-state';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';

@Component({
  selector: 'app-semester-manage',
  templateUrl: './semester-manage.component.html',
  styleUrl: './semester-manage.component.scss',
})
export class SemesterManageComponent
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
