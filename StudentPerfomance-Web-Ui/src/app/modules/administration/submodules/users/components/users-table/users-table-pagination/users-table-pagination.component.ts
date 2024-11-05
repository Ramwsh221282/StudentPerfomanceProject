import { Component, OnInit } from '@angular/core';
import { UsersPaginationService } from './users-pagination.servce';

@Component({
  selector: 'app-users-table-pagination',
  templateUrl: './users-table-pagination.component.html',
  styleUrl: './users-table-pagination.component.scss',
})
export class UsersTablePaginationComponent implements OnInit {
  public constructor(
    protected readonly paginationService: UsersPaginationService
  ) {}

  public ngOnInit(): void {
    this.paginationService.refreshPagination();
  }
}
