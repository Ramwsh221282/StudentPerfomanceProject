import { Component, OnInit } from '@angular/core';
import { DepartmentDataService } from './department-data.service';
import { DepartmentPaginationService } from './department-pagination.service';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { Department } from '../../models/departments.interface';

@Component({
  selector: 'app-department-table',
  templateUrl: './department-table.component.html',
  styleUrl: './department-table.component.scss',
  providers: [
    DepartmentPaginationService,
    DepartmentDataService,
    UserOperationNotificationService,
    DepartmentPaginationService,
  ],
})
export class DepartmentTableComponent implements OnInit {
  protected creationModalVisibility: boolean;
  protected filterModalVisibility: boolean;

  protected isSuccess: boolean;
  protected isFailure: boolean;

  protected departments: Department[];

  public constructor(
    protected readonly paginationService: DepartmentPaginationService,
    protected readonly dataService: DepartmentDataService,
    protected readonly notificationService: UserOperationNotificationService,
  ) {
    this.isSuccess = false;
    this.isFailure = false;
    this.creationModalVisibility = false;
    this.filterModalVisibility = false;
    this.departments = [];
  }

  public ngOnInit(): void {
    this.dataService.addPages(
      this.paginationService.currentPage,
      this.paginationService.pageSize,
    );
    this.dataService.fetch().subscribe((response) => {
      this.departments = response;
    });
  }
}
