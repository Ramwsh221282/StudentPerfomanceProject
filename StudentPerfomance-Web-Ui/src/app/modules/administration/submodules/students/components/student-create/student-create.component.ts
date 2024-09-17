import { Component, Input, OnInit } from '@angular/core';
import { BaseStudentForm } from '../../models/base-student-form';
import { FacadeStudentService } from '../../services/facade-student.service';
import { StudentCreationHandler } from './student-creation-handler';
import { catchError, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { INotificatable } from '../../../../../../shared/models/inotificated-component-interface/inotificatable.interface';
import { ModalState } from '../../../../../../shared/models/modals/modal-state';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { StudentGroup } from '../../../student-groups/services/studentsGroup.interface';

@Component({
  selector: 'app-student-create',
  templateUrl: './student-create.component.html',
  styleUrl: './student-create.component.scss',
})
export class StudentCreateComponent
  extends BaseStudentForm
  implements OnInit, INotificatable
{
  @Input({ required: true }) public currentGroup: StudentGroup;
  public readonly successModalState: ModalState = new ModalState();
  public readonly failureModalState: ModalState = new ModalState();

  public constructor(
    private readonly _facadeService: FacadeStudentService,
    private readonly _notificationService: UserOperationNotificationService
  ) {
    super('Создание студента');
  }

  public ngOnInit(): void {
    this.initForm();
  }

  protected override submit(): void {
    const handler = StudentCreationHandler(
      this._facadeService,
      this,
      this._notificationService
    );
    this._facadeService
      .create(this.createStudentFromForm(this.currentGroup))
      .pipe(
        tap((response) => handler.handle(response)),
        catchError((error: HttpErrorResponse) => handler.handleError(error))
      )
      .subscribe();
    this.initForm();
  }
}
