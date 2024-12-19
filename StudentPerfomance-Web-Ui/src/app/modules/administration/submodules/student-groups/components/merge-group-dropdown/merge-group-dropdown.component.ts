import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { StudentGroup } from '../../services/studentsGroup.interface';
import { ISubbmittable } from '../../../../../../shared/models/interfaces/isubbmitable';
import { StudentGroupsFacadeService } from '../../services/student-groups-facade.service';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { StudentGroupSearchService } from '../../services/student-group-search.service';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-merge-group-dropdown',
  templateUrl: './merge-group-dropdown.component.html',
  styleUrl: './merge-group-dropdown.component.scss',
})
export class MergeGroupDropdownComponent implements ISubbmittable, OnInit {
  @Input({ required: true }) visibility: boolean = false;
  @Input({ required: true }) group: StudentGroup;
  @Output() visibilityChange: EventEmitter<boolean> = new EventEmitter();
  @Output() groupMerged: EventEmitter<void> = new EventEmitter();

  protected groups: StudentGroup[] = [];
  protected groupNames: string[] = [];
  protected selectOtherGroupLabel: string = 'Выберите другую группу';
  protected isSelectingOtherGroup: boolean = false;
  protected selectedGroup: StudentGroup | null;

  public constructor(
    private readonly _facadeService: StudentGroupsFacadeService,
    private readonly _notificationService: UserOperationNotificationService,
    private readonly _groupsSearchService: StudentGroupSearchService,
  ) {}

  public ngOnInit(): void {
    this._groupsSearchService.getAllGroups().subscribe((response) => {
      this.groups = response;
      for (const group of this.groups) {
        if (group.name == this.group.name) continue;
        this.groupNames.push(group.name);
      }
    });
  }

  public submit(): void {
    if (this.isOtherGroupWasNotSelected()) return;
    const groupAName = this.group.name;
    const groupBName = this.selectedGroup!.name;
    this._facadeService
      .merge(this.group, this.selectedGroup!)
      .pipe(
        tap((response) => {
          this._notificationService.SetMessage = `Группа ${groupAName} смешана с группой ${groupBName}`;
          this.groupMerged.emit();
          this.close();
        }),
        catchError((error: HttpErrorResponse) => {
          this._notificationService.SetMessage = error.error;
          this._notificationService.failure();
          this.close();
          return new Observable<never>();
        }),
      )
      .subscribe();
  }

  protected close(): void {
    this.visibility = false;
    this.visibilityChange.emit(this.visibility);
  }

  protected otherGroupSelectedHandler(groupName: string): void {
    this.selectOtherGroupLabel = groupName;
    this.selectedGroup! = this.groups.find((group) => group.name == groupName)!;
  }

  private isOtherGroupWasNotSelected(): boolean {
    if (this.selectedGroup == null) {
      this._notificationService.SetMessage = 'Другая группа не была выбрана';
      this._notificationService.failure();
      return true;
    }
    return false;
  }
}
