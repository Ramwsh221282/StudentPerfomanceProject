import { Component, EventEmitter, Input, Output } from '@angular/core';
import { StudentGroup } from '../../services/studentsGroup.interface';
import { ISubbmittable } from '../../../../../../shared/models/interfaces/isubbmitable';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { StudentGroupsCreateDataService } from '../../services/student-groups-create-data.service';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-create-group-dropdown',
  templateUrl: './create-group-dropdown.component.html',
  styleUrl: './create-group-dropdown.component.scss',
})
export class CreateGroupDropdownComponent implements ISubbmittable {
  @Input({ required: true }) visibility: boolean = false;
  @Output() visibilityChange: EventEmitter<boolean> = new EventEmitter();
  @Output() groupCreated: EventEmitter<StudentGroup> = new EventEmitter();
  protected groupName: string = '';

  public constructor(
    private readonly _notificationService: UserOperationNotificationService,
    private readonly _createService: StudentGroupsCreateDataService,
  ) {}

  protected close(): void {
    this.visibility = false;
    this.visibilityChange.emit(this.visibility);
  }

  public submit(): void {
    if (this.isGroupNameEmpty()) return;
    const group = this.createEmptyGroup();
    this._createService
      .create(group)
      .pipe(
        tap((response) => {
          group.entityNumber = response.entityNumber;
          this.groupCreated.emit(group);
          this._notificationService.SetMessage = `Добавлена новая группа ${group.name}`;
          this._notificationService.success();
          this.cleanInputs();
        }),
        catchError((error: HttpErrorResponse) => {
          this._notificationService.SetMessage = error.error;
          this._notificationService.failure();
          return new Observable<never>();
        }),
      )
      .subscribe();
  }

  private createEmptyGroup(): StudentGroup {
    const group = {} as StudentGroup;
    group.name = this.groupName;
    return group;
  }

  private isGroupNameEmpty(): boolean {
    if (this.groupName.length == 0 || this.groupName.trim().length == 0) {
      this._notificationService.SetMessage = 'Название группы не указано';
      this._notificationService.success();
      return true;
    }
    return false;
  }

  private cleanInputs(): void {
    this.groupName = '';
  }
}
