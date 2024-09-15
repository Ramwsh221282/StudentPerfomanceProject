import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MergeStudentGroupForm } from '../../models/merge-student-group-form';
import { SemestersSearchGroupModalFormComponent } from '../../../dbmanager-semesters-view/dbmanager-semesters-control-panel/semesters-search-group-modal-form/semesters-search-group-modal-form.component';
import { NgIf } from '@angular/common';
import { INotificatable } from '../../../../shared-models/interfaces/inotificated-component-interface/inotificatable.interface';
import { UserOperationNotificationService } from '../../../../shared-services/user-notifications/user-operation-notification-service.service';
import { StudentGroupMergeHandler } from './student-group-merge-handler';
import { catchError, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { StudentGroupsFacadeService } from '../../services/student-groups-facade.service';
import { SuccessNotificationFormComponent } from '../../../../notification-modal-forms/success-notification-form/success-notification-form.component';
import { FailureNotificationFormComponent } from '../../../../notification-modal-forms/failure-notification-form/failure-notification-form.component';
import { ModalState } from '../../../../shared-models/models/modals/modal-state';

@Component({
  selector: 'app-dbmanager-studentgroups-merge-group-form',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    FormsModule,
    SemestersSearchGroupModalFormComponent,
    NgIf,
    SuccessNotificationFormComponent,
    FailureNotificationFormComponent,
  ],
  templateUrl: './dbmanager-studentgroups-merge-group-form.component.html',
  styleUrl: './dbmanager-studentgroups-merge-group-form.component.scss',
  providers: [],
})
export class DbmanagerStudentgroupsMergeGroupFormComponent
  extends MergeStudentGroupForm
  implements OnInit, INotificatable
{
  public readonly successModalState: ModalState = new ModalState();
  public readonly failureModalState: ModalState = new ModalState();
  protected modalInputA: string;
  protected modalInputB: string;
  protected isGroupSelectionAOpened: boolean = false;
  protected isGroupSelectionBOpened: boolean = false;

  public constructor(
    private readonly _facadeService: StudentGroupsFacadeService,
    private readonly _notificationService: UserOperationNotificationService
  ) {
    super('Объединение групп');
  }

  public ngOnInit(): void {
    this.initForm();
    this.modalInputA = '';
    this.modalInputB = '';
  }

  protected openSelectionA(): void {
    this.isGroupSelectionAOpened = true;
  }

  protected openSelectionB(): void {
    this.isGroupSelectionBOpened = true;
  }

  protected checkModalState(state: boolean): void {
    this.isGroupSelectionAOpened = state;
    this.isGroupSelectionBOpened = state;
  }

  protected getSelectedGroupAFromChild(selectedGroupName: string): void {
    this.modalInputA = selectedGroupName;
    this.form.value['groupNameA'] = this.modalInputA;
  }

  protected getSelectedGroupBFromChild(selectedGroupName: string): void {
    this.modalInputB = selectedGroupName;
    this.form.value['groupNameB'] = this.modalInputB;
  }

  protected override submit(): void {
    const groupA = super.createGroupFromForm('groupNameA');
    const groupB = super.createGroupFromForm('groupNameB');
    const handler = StudentGroupMergeHandler(
      groupA,
      groupB,
      this._facadeService,
      this,
      this._notificationService
    );
    this._facadeService
      .merge(groupA, groupB)
      .pipe(
        tap((response) => handler.handle(response)),
        catchError((error: HttpErrorResponse) => handler.handleError(error))
      )
      .subscribe();
    this.ngOnInit();
  }
}
