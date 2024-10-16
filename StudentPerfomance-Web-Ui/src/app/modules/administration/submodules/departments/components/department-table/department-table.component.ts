import { Component, OnInit } from '@angular/core';
import { DepartmentDataService } from './department-data.service';
import { DepartmentPaginationService } from './department-pagination.service';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';

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

  public constructor(
    protected readonly paginationService: DepartmentPaginationService,
    protected readonly dataService: DepartmentDataService,
    protected readonly notificationService: UserOperationNotificationService
  ) {
    this.isSuccess = false;
    this.isFailure = false;
    this.creationModalVisibility = false;
    this.filterModalVisibility = false;
  }

  public ngOnInit(): void {
    this.refreshPagination();
    this.appendPages();
    this.fetchData();
  }

  protected appendPages(): void {
    this.dataService.addPages(
      this.paginationService.currentPage,
      this.paginationService.pageSize
    );
  }

  protected refreshPagination(): void {
    this.paginationService.refreshPagination();
  }

  protected fetchData(): void {
    this.dataService.fetch();
  }

  protected refreshData(): void {
    this.refreshPagination();
    this.appendPages();
    this.fetchData();
  }
}
