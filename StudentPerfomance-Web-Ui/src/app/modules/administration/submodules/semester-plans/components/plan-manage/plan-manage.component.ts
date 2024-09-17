import { Component, Input, OnInit } from '@angular/core';
import { Semester } from '../../../semesters/models/semester.interface';
import { SemesterPlanFacadeService } from '../../services/semester-plan-facade.service';
import { SemesterPlanDeletionHandler } from './semester-plan-deletion-handler';
import { HttpErrorResponse } from '@angular/common/http';
import { catchError, tap } from 'rxjs';
import { SemesterPlanBaseForm } from '../../models/semester-plan-base.form';
import { INotificatable } from '../../../../../../shared/models/inotificated-component-interface/inotificatable.interface';
import { ModalState } from '../../../../../../shared/models/modals/modal-state';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';

@Component({
  selector: 'app-plan-manage',
  templateUrl: './plan-manage.component.html',
  styleUrl: './plan-manage.component.scss',
})
export class PlanManageComponent
  extends SemesterPlanBaseForm
  implements OnInit, INotificatable
{
  @Input({ required: true }) public currentSemester: Semester;
  public readonly successModalState: ModalState = new ModalState();
  public readonly failureModalState: ModalState = new ModalState();
  public constructor(
    protected readonly facadeService: SemesterPlanFacadeService,
    private readonly _notificationService: UserOperationNotificationService
  ) {
    super('Управление дисциплиной');
  }

  public ngOnInit(): void {
    this.initForm();
  }

  protected override submit(): void {
    const handler = SemesterPlanDeletionHandler(
      this.facadeService,
      this._notificationService,
      this
    );
    this.facadeService
      .delete(this.createSemesterPlanFromForm(this.currentSemester))
      .pipe(
        tap((response) => handler.handle(response)),
        catchError((error: HttpErrorResponse) => handler.handleError(error))
      )
      .subscribe();
    this.ngOnInit();
  }
}
