import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ISuccessNotificatable } from '../../../../../../../shared/models/interfaces/isuccess-notificatable';
import { IFailureNotificatable } from '../../../../../../../shared/models/interfaces/ifailure-notificatable';
import { ISubbmittable } from '../../../../../../../shared/models/interfaces/isubbmitable';
import { StudentGroup } from '../../../services/studentsGroup.interface';
import { UserOperationNotificationService } from '../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { StudentGroupSearchService } from '../../../services/student-group-search.service';
import { StudentGroupBuilder } from '../../../models/student-group-builder';
import { StudentGroupsMergeDataService } from '../../../services/student-groups-merge-data.service';
import { StudentGroupMergeHandler } from './merge-group-handler';
import { catchError, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-merge-group-modal',
  templateUrl: './merge-group-modal.component.html',
  styleUrl: './merge-group-modal.component.scss',
  providers: [
    UserOperationNotificationService,
    StudentGroupSearchService,
    StudentGroupsMergeDataService,
  ],
})
export class MergeGroupModalComponent
  implements
    OnInit,
    ISuccessNotificatable,
    IFailureNotificatable,
    ISubbmittable
{
  @Input({ required: true }) initial: StudentGroup;
  @Output() visibility: EventEmitter<boolean> = new EventEmitter<boolean>();

  protected activeGroup: StudentGroup;
  protected filterGroup: StudentGroup;
  protected groups: StudentGroup[];

  protected isSuccess: boolean;
  protected isFailure: boolean;

  public constructor(
    protected readonly notificationService: UserOperationNotificationService,
    private readonly _searchService: StudentGroupSearchService,
    private readonly _mergeService: StudentGroupsMergeDataService
  ) {
    this.isSuccess = false;
    this.isFailure = false;
    this.activeGroup = {} as StudentGroup;
    this.filterGroup = {} as StudentGroup;
    this.groups = [];
  }

  public submit(): void {
    const handler = StudentGroupMergeHandler(
      this.notificationService,
      this,
      this,
      this.activeGroup
    );
    this._mergeService
      .merge(this.initial, this.activeGroup)
      .pipe(
        tap((response) => handler.handle(response)),
        catchError((error: HttpErrorResponse) => handler.handleError(error))
      )
      .subscribe();
  }

  public notifyFailure(): void {
    this.isFailure = true;
  }

  public manageFailure(value: boolean) {
    this.isFailure = value;
  }

  public manageSuccess(value: boolean): void {
    this.isSuccess = value;
  }

  public notifySuccess(): void {
    this.isSuccess = true;
  }

  public ngOnInit(): void {
    const builder: StudentGroupBuilder = new StudentGroupBuilder();
    this.activeGroup = { ...builder.buildDefault() };
    this.filterGroup = { ...builder.buildDefault() };
    this.fetchAllGroups();
  }

  protected fetchAllGroups(): void {
    this._searchService.getAllGroups().subscribe((response) => {
      this.groups = response;
    });
  }

  protected searchGroups(): void {
    this._searchService.searchGroups(this.filterGroup).subscribe((response) => {
      this.groups = response;
    });
  }

  protected close(): void {
    this.visibility.emit(false);
  }

  protected selectGroup(group: StudentGroup): void {
    this.activeGroup = { ...group };
  }
}
