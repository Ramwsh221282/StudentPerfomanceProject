import { Component, Input, OnInit } from '@angular/core';
import { SemesterPlanBaseForm } from '../../models/semester-plan-base.form';
import { Semester } from '../../../semesters/models/semester.interface';
import { SemesterPlanFacadeService } from '../../services/semester-plan-facade.service';
import { SemesterPlanCreationHandler } from './semester-plan-creation-handler';
import { HttpErrorResponse } from '@angular/common/http';
import { catchError, tap } from 'rxjs';
import { INotificatable } from '../../../../../../shared/models/inotificated-component-interface/inotificatable.interface';
import { ModalState } from '../../../../../../shared/models/modals/modal-state';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';

@Component({
  selector: 'app-plan-create',
  templateUrl: './plan-create.component.html',
  styleUrl: './plan-create.component.scss',
})
export class PlanCreateComponent
  extends SemesterPlanBaseForm
  implements OnInit, INotificatable
{
  @Input({ required: true }) public currentSemester: Semester;
  public readonly successModalState: ModalState = new ModalState();
  public readonly failureModalState: ModalState = new ModalState();
  public constructor(
    private readonly _facadeService: SemesterPlanFacadeService,
    private readonly _notificationService: UserOperationNotificationService
  ) {
    super('Добавление дисциплины');
  }

  public ngOnInit(): void {
    this.initForm();
  }

  protected override submit(): void {
    const handler = SemesterPlanCreationHandler(
      this._facadeService,
      this._notificationService,
      this
    );
    this._facadeService
      .create(this.createSemesterPlanFromForm(this.currentSemester))
      .pipe(
        tap((response) => handler.handle(response)),
        catchError((error: HttpErrorResponse) => handler.handleError(error))
      )
      .subscribe();
    this.ngOnInit();
  }
}
