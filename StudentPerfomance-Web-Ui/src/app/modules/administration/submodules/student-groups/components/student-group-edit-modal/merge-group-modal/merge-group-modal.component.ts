import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ISubbmittable } from '../../../../../../../shared/models/interfaces/isubbmitable';
import { StudentGroup } from '../../../services/studentsGroup.interface';
import { StudentGroupSearchService } from '../../../services/student-group-search.service';
import { StudentGroupBuilder } from '../../../models/student-group-builder';
import { StudentGroupsMergeDataService } from '../../../services/student-groups-merge-data.service';
import { StudentGroupMergeHandler } from './merge-group-handler';
import { catchError, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { UserOperationNotificationService } from '../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';

@Component({
  selector: 'app-merge-group-modal',
  templateUrl: './merge-group-modal.component.html',
  styleUrl: './merge-group-modal.component.scss',
  providers: [StudentGroupSearchService, StudentGroupsMergeDataService],
})
export class MergeGroupModalComponent implements OnInit, ISubbmittable {
  @Input({ required: true }) initial: StudentGroup;
  @Output() visibility: EventEmitter<boolean> = new EventEmitter<boolean>();
  @Output() successEmitter: EventEmitter<void> = new EventEmitter();
  @Output() failureEmitter: EventEmitter<void> = new EventEmitter();
  @Output() refreshEmitter: EventEmitter<void> = new EventEmitter();

  protected activeGroup: StudentGroup;
  protected filterGroup: StudentGroup;
  protected groups: StudentGroup[];

  public constructor(
    private readonly _searchService: StudentGroupSearchService,
    private readonly _mergeService: StudentGroupsMergeDataService,
    private readonly _notificationService: UserOperationNotificationService,
  ) {
    this.activeGroup = {} as StudentGroup;
    this.filterGroup = {} as StudentGroup;
    this.groups = [];
  }

  public submit(): void {
    const handler = StudentGroupMergeHandler(
      this._notificationService,
      this.successEmitter,
      this.failureEmitter,
      this.refreshEmitter,
      this.activeGroup,
    );
    this._mergeService
      .merge(this.initial, this.activeGroup)
      .pipe(
        tap((response) => handler.handle(response)),
        catchError((error: HttpErrorResponse) => handler.handleError(error)),
      )
      .subscribe();
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
