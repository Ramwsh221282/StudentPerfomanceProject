import { Component, EventEmitter, Input, Output } from '@angular/core';
import { StudentGroupsFacadeService } from '../../services/student-groups-facade.service';
import { AuthService } from '../../../../../users/services/auth.service';
import { AppConfigService } from '../../../../../../app.config.service';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { StudentGroup } from '../../services/studentsGroup.interface';
import { FilterFetchPolicy } from '../../models/fetch-policies/filter-fetch-policy';
import { DefaultFetchPolicy } from '../../models/fetch-policies/default-fetch-policy';

@Component({
  selector: 'app-filter-group-dropdown',
  templateUrl: './filter-group-dropdown.component.html',
  styleUrl: './filter-group-dropdown.component.scss',
})
export class FilterGroupDropdownComponent {
  @Input({ required: true }) visibility: boolean = false;
  @Output() visibilityChange: EventEmitter<boolean> = new EventEmitter();
  @Output() filtered: EventEmitter<void> = new EventEmitter();

  protected groupName: string = '';

  public constructor(
    private readonly _facadeService: StudentGroupsFacadeService,
    private readonly _authService: AuthService,
    private readonly _appConfig: AppConfigService,
    private readonly _notificationService: UserOperationNotificationService,
  ) {}

  public submit(): void {
    if (this.isGroupNameEmpty()) return;
    const group = this.createGroupForFilter();
    this._facadeService.setPolicy(
      new FilterFetchPolicy(group, this._authService, this._appConfig),
    );
    this.filtered.emit();
    this.close();
  }

  protected cancel(): void {
    this._facadeService.setPolicy(
      new DefaultFetchPolicy(this._authService, this._appConfig),
    );
    this.filtered.emit();
    this.close();
  }

  protected close(): void {
    this.visibility = false;
    this.visibilityChange.emit(this.visibility);
  }

  private createGroupForFilter(): StudentGroup {
    const group = {} as StudentGroup;
    group.name = this.groupName;
    return group;
  }

  private isGroupNameEmpty(): boolean {
    if (this.groupName.length == 0 || this.groupName.trim().length == 0) {
      this._notificationService.SetMessage =
        'Не указано название группы для фильтра';
      this._notificationService.failure();
      return true;
    }
    return false;
  }
}
