import { Component, OnInit } from '@angular/core';
import { MergeStudentGroupForm } from '../../models/merge-student-group-form';
import { StudentGroupsFacadeService } from '../../services/student-groups-facade.service';
import { StudentGroupMergeHandler } from './student-group-merge-handler';
import { HttpErrorResponse } from '@angular/common/http';
import { tap, catchError } from 'rxjs';
import { INotificatable } from '../../../../../../shared/models/inotificated-component-interface/inotificatable.interface';
import { ModalState } from '../../../../../../shared/models/modals/modal-state';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';

@Component({
  selector: 'app-merge-group',
  templateUrl: './merge-group.component.html',
  styleUrl: './merge-group.component.scss',
})
export class MergeGroupComponent
  extends MergeStudentGroupForm
  implements OnInit, INotificatable
{
  public successModalState: ModalState = new ModalState();
  public failureModalState: ModalState = new ModalState();
  protected modalInputA: string;
  protected modalInputB: string;
  protected isGroupSelectionAOpened: boolean = false;
  protected isGroupSelectionBOpened: boolean = false;
  public constructor(
    private readonly _facadeService: StudentGroupsFacadeService,
    private readonly _notificationService: UserOperationNotificationService
  ) {
    super('Смешивание групп');
  }

  public ngOnInit(): void {
    this.initForm();
    this.modalInputA = '';
    this.modalInputB = '';
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
}
