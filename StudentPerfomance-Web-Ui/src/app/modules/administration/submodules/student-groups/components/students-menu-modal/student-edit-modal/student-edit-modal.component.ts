import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { StudentBaseForm } from '../student-creation-modal/student-base-form';
import { ISubbmittable } from '../../../../../../../shared/models/interfaces/isubbmitable';
import { ISuccessNotificatable } from '../../../../../../../shared/models/interfaces/isuccess-notificatable';
import { IFailureNotificatable } from '../../../../../../../shared/models/interfaces/ifailure-notificatable';
import { Student } from '../../../../students/models/student.interface';
import { StudentEditService } from './student-edit.service';
import { UserOperationNotificationService } from '../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { StudentEditHandler } from './student-edit-handler';
import { catchError, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { StudentGroup } from '../../../services/studentsGroup.interface';

@Component({
  selector: 'app-student-edit-modal',
  templateUrl: './student-edit-modal.component.html',
  styleUrl: './student-edit-modal.component.scss',
  providers: [UserOperationNotificationService, StudentEditService],
})
export class StudentEditModalComponent
  extends StudentBaseForm
  implements
    ISubbmittable,
    ISuccessNotificatable,
    IFailureNotificatable,
    OnInit
{
  @Input({ required: true }) initial: Student;
  @Output() visibility: EventEmitter<boolean> = new EventEmitter();
  protected activeStudent: Student;
  protected copy: Student;
  protected activeState: string;
  protected activeGroup: StudentGroup;

  protected isSuccess: boolean;
  protected isFailure: boolean;
  protected groupSelectionModalVisibility: boolean;

  public constructor(
    private readonly _editService: StudentEditService,
    protected readonly notificationService: UserOperationNotificationService
  ) {
    super();
    this.groupSelectionModalVisibility = false;
  }

  public ngOnInit(): void {
    this.activeStudent = { ...this.initial };
    this.copy = { ...this.initial };
    this.activeState = this.initial.state;
    this.activeGroup = { ...this.initial.group };
    this.initForm();
  }

  public notifyFailure(): void {
    this.isFailure = true;
  }

  public manageFailure(value: boolean): void {
    this.isFailure = value;
    this.close();
  }

  public notifySuccess(): void {
    this.isSuccess = true;
  }

  public manageSuccess(value: boolean): void {
    this.isSuccess = value;
    this.close();
  }

  public submit(): void {
    const updated: Student = this.createStudentFromForm(this.initial.group);
    updated.state = this.activeState;
    updated.group = this.activeGroup;
    console.log(updated);
    console.log(this.initial);
    const handler = StudentEditHandler(
      this.notificationService,
      this,
      this,
      this.initial
    );

    this._editService
      .update(this.initial, updated)
      .pipe(
        tap((response) => handler.handle(response)),
        catchError((error: HttpErrorResponse) => handler.handleError(error))
      )
      .subscribe();
  }

  protected close(): void {
    this.visibility.emit(false);
  }

  protected selectState(state: string): void {
    this.activeState = state;
  }

  protected selectGroup(group: StudentGroup): void {
    this.activeGroup = { ...group };
  }

  protected openGroupSelectionModal(): void {
    this.groupSelectionModalVisibility = true;
  }

  protected closeGroupSelectionModal(value: boolean): void {
    this.groupSelectionModalVisibility = value;
  }
}
