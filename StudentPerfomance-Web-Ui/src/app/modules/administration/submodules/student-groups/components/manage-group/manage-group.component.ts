import { Component, OnInit } from '@angular/core';
import { BaseStudentGroupForm } from '../../models/base-student-group-form';
import { StudentGroupsFacadeService } from '../../services/student-groups-facade.service';
import { StudentGroupDeletionHandler } from './studentgroup-deletion-handler';
import { catchError, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { StudentGroupUpdateHandler } from './studentgroup-update-handler';
import { StudentGroup } from '../../services/studentsGroup.interface';
import { INotificatable } from '../../../../../../shared/models/inotificated-component-interface/inotificatable.interface';
import { ModalState } from '../../../../../../shared/models/modals/modal-state';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';

@Component({
  selector: 'app-manage-group',
  templateUrl: './manage-group.component.html',
  styleUrl: './manage-group.component.scss',
})
export class ManageGroupComponent
  extends BaseStudentGroupForm
  implements OnInit, INotificatable
{
  public readonly successModalState: ModalState = new ModalState();
  public readonly failureModalState: ModalState = new ModalState();

  public constructor(
    protected readonly facadeService: StudentGroupsFacadeService,
    private readonly _notificationService: UserOperationNotificationService
  ) {
    super('Редактирование группы');
  }

  public ngOnInit(): void {
    this.initForm();
  }

  protected override submit(): void {
    this.ngOnInit();
  }

  protected delectionClick(group: StudentGroup): void {
    const handler = StudentGroupDeletionHandler(
      this.facadeService,
      this,
      this._notificationService
    );
    this.facadeService
      .delete(group)
      .pipe(
        tap((response) => handler.handle(response)),
        catchError((error: HttpErrorResponse) => handler.handleError(error))
      )
      .subscribe();
    this.submit();
  }

  protected editClick(oldGroup: StudentGroup): void {
    const handler = StudentGroupUpdateHandler(
      this.facadeService,
      this,
      oldGroup,
      this._notificationService
    );
    this.facadeService
      .update(this.createStudentGroupFromForm())
      .pipe(
        tap((response) => handler.handle(response)),
        catchError((error: HttpErrorResponse) => handler.handleError(error))
      )
      .subscribe();
    this.submit();
  }
}
