import { Component, OnInit } from '@angular/core';
import { BaseSemesterForm } from '../../models/semester-form-base';
import { SemesterFacadeService } from '../../services/semester-facade.service';
import { SemesterCreatedHandler } from './create-semester-handler';
import { HttpErrorResponse } from '@angular/common/http';
import { catchError, tap } from 'rxjs';
import { INotificatable } from '../../../../../../shared/models/inotificated-component-interface/inotificatable.interface';
import { ModalState } from '../../../../../../shared/models/modals/modal-state';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';

@Component({
  selector: 'app-semester-create',
  templateUrl: './semester-create.component.html',
  styleUrl: './semester-create.component.scss',
})
export class SemesterCreateComponent
  extends BaseSemesterForm
  implements OnInit, INotificatable
{
  public readonly successModalState: ModalState = new ModalState();
  public readonly failureModalState: ModalState = new ModalState();
  protected selectedStudentGroupFromChild: string;
  protected selectedSemesterNumberValueFromChild: number;
  protected isNumberSelectionModalOpened: boolean;
  protected isGroupSelectionModalOpened: boolean;

  public constructor(
    private readonly _facadeService: SemesterFacadeService,
    private readonly _notificationService: UserOperationNotificationService
  ) {
    super('Добавление семестра');
  }

  public ngOnInit(): void {
    this.initForm();
    this.isNumberSelectionModalOpened = false;
    this.isGroupSelectionModalOpened = false;
    this.selectedStudentGroupFromChild = '';
    this.selectedSemesterNumberValueFromChild = 0;
  }

  protected openNumberSelectionModal(): void {
    this.isNumberSelectionModalOpened = true;
  }

  protected openGroupSelectionModal(): void {
    this.isGroupSelectionModalOpened = true;
  }

  protected checkModalState(state: boolean): void {
    this.isGroupSelectionModalOpened = state;
    this.isNumberSelectionModalOpened = state;
  }

  protected getSelectedGroupFromChild(selectedGroupName: string): void {
    this.selectedStudentGroupFromChild = selectedGroupName;
    this.form.value['groupName'] = this.selectedStudentGroupFromChild;
  }

  protected getSelectedNumberFromChild(selectedSemesterNumber: number): void {
    this.selectedSemesterNumberValueFromChild = selectedSemesterNumber;
    this.form.value['number'] = this.selectedSemesterNumberValueFromChild;
  }

  protected override submit(): void {
    const handler = SemesterCreatedHandler(
      this._facadeService,
      this,
      this._notificationService
    );
    this._facadeService
      .create(this.createSemesterFromForm())
      .pipe(
        tap((response) => handler.handle(response)),
        catchError((error: HttpErrorResponse) => handler.handleError(error))
      )
      .subscribe();
    this.ngOnInit();
  }
}
