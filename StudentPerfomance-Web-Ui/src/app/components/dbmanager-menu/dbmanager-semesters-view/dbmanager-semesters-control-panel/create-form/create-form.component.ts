import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { SemestersSearchGroupModalFormComponent } from '../semesters-search-group-modal-form/semesters-search-group-modal-form.component';
import { NgIf } from '@angular/common';
import { SemestersSelectNumberModalFormComponent } from '../semesters-select-number-modal-form/semesters-select-number-modal-form.component';
import { BaseSemesterForm } from '../../models/semester-form-base';
import { SemesterFacadeService } from '../../services/semester-facade.service';
import { UserOperationNotificationService } from '../../../../shared-services/user-notifications/user-operation-notification-service.service';
import { INotificatable } from '../../../../shared-models/interfaces/inotificated-component-interface/inotificatable.interface';
import { SemesterCreatedHandler } from './create-semester-handler';
import { catchError, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { SuccessNotificationFormComponent } from '../../../../notification-modal-forms/success-notification-form/success-notification-form.component';
import { FailureNotificationFormComponent } from '../../../../notification-modal-forms/failure-notification-form/failure-notification-form.component';
import { ModalState } from '../../../../shared-models/models/modals/modal-state';

@Component({
  selector: 'app-create-form',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    SemestersSearchGroupModalFormComponent,
    NgIf,
    FormsModule,
    SemestersSelectNumberModalFormComponent,
    SuccessNotificationFormComponent,
    FailureNotificationFormComponent,
  ],
  templateUrl: './create-form.component.html',
  styleUrl: './create-form.component.scss',
})
export class CreateFormComponent
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
