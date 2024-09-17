import { Component, OnInit } from '@angular/core';
import { BaseStudentGroupForm } from '../../models/base-student-group-form';
import { StudentGroupsFacadeService } from '../../services/student-groups-facade.service';
import { catchError, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { StudentGroupCreationHandler } from './group-creation-handler';
import { INotificatable } from '../../../../../../shared/models/inotificated-component-interface/inotificatable.interface';
import { ModalState } from '../../../../../../shared/models/modals/modal-state';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';

@Component({
  selector: 'app-create-group',
  templateUrl: './create-group.component.html',
  styleUrl: './create-group.component.scss',
  providers: [UserOperationNotificationService],
})
export class CreateGroupComponent
  extends BaseStudentGroupForm
  implements OnInit, INotificatable
{
  public successModalState: ModalState = new ModalState();
  public failureModalState: ModalState = new ModalState();
  public constructor(
    private readonly _facadeService: StudentGroupsFacadeService,
    private readonly _notificationService: UserOperationNotificationService
  ) {
    super('Создание группы');
  }

  public ngOnInit(): void {
    this.initForm();
  }

  protected override submit(): void {
    const handler = StudentGroupCreationHandler(
      this._facadeService,
      this,
      this._notificationService
    );
    this._facadeService
      .create(this.createStudentGroupFromForm())
      .pipe(
        tap((response) => handler.handle(response)),
        catchError((error: HttpErrorResponse) => handler.handleError(error))
      )
      .subscribe();
    this.ngOnInit();
  }
}
