import { Component, EventEmitter, Input, Output } from '@angular/core';
import { UserOperationNotificationService } from '../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { AppConfigService } from '../../../../../../../app.config.service';
import { AuthService } from '../../../../../../users/services/auth.service';
import { IsNullOrWhiteSpace } from '../../../../../../../shared/utils/string-helper';
import { ISubbmittable } from '../../../../../../../shared/models/interfaces/isubbmitable';
import { DepartmentDataService } from '../department-data.service';
import { DepartmentFilterFetchPolicy } from '../../../models/fetch-policies/filter-fetch-policy';
import { Department } from '../../../models/departments.interface';
import { DepartmentDefaultFetchPolicy } from '../../../models/fetch-policies/default-fetch-policy';

@Component({
  selector: 'app-department-filter-dropdown',
  templateUrl: './department-filter-dropdown.component.html',
  styleUrl: './department-filter-dropdown.component.scss',
})
export class DepartmentFilterDropdownComponent implements ISubbmittable {
  @Input({ required: true }) visibility: boolean = false;
  @Output() visibilityChange: EventEmitter<boolean> = new EventEmitter();
  @Output() filtered: EventEmitter<void> = new EventEmitter();

  protected name: string = '';

  public constructor(
    private readonly _notificationService: UserOperationNotificationService,
    private readonly _appConfigService: AppConfigService,
    private readonly _authService: AuthService,
    private readonly _departmentDataService: DepartmentDataService,
  ) {}

  public submit(): void {
    if (this.isNameEmpty()) return;
    const department = this.createDepartment();
    this._departmentDataService.setPolicy(
      new DepartmentFilterFetchPolicy(
        department,
        this._authService,
        this._appConfigService,
      ),
    );
    this.filtered.emit();
    this.close();
  }

  protected cancel(): void {
    this._departmentDataService.setPolicy(
      new DepartmentDefaultFetchPolicy(
        this._authService,
        this._appConfigService,
      ),
    );
    this.filtered.emit();
    this.close();
  }

  protected close(): void {
    this.visibility = false;
    this.visibilityChange.emit(this.visibility);
  }

  private isNameEmpty(): boolean {
    if (IsNullOrWhiteSpace(this.name)) {
      this._notificationService.SetMessage = 'Название кафедры было пустым';
      this._notificationService.failure();
      return true;
    }
    return false;
  }

  private createDepartment(): Department {
    const department = {} as Department;
    department.name = this.name;
    return department;
  }
}
