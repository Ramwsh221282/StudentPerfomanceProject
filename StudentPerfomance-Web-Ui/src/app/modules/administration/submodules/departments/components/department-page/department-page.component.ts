import { Component, Input, OnInit } from '@angular/core';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { DepartmentPaginationService } from '../department-table/department-pagination.service';
import { DepartmentDataService } from '../department-table/department-data.service';
import { Department } from '../../models/departments.interface';

@Component({
  selector: 'app-department-page',
  templateUrl: './department-page.component.html',
  styleUrl: './department-page.component.scss',
  providers: [
    UserOperationNotificationService,
    DepartmentPaginationService,
    DepartmentDataService,
  ],
})
export class DepartmentPageComponent implements OnInit {
  @Input() departments: Department[];

  protected selectedDepartment: Department | null;

  public constructor(
    protected readonly notificationService: UserOperationNotificationService,
    private readonly _dataService: DepartmentDataService,
    protected readonly paginationService: DepartmentPaginationService,
  ) {}

  public ngOnInit(): void {
    this._dataService.addPages(
      this.paginationService.currentPage,
      this.paginationService.pageSize,
    );
    this._dataService.fetch().subscribe((response) => {
      this.departments = response;
    });
  }
}
