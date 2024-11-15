import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { UsersPaginationService } from './users-pagination.servce';
import { UsersDataService } from '../../../services/users-data.service';

@Component({
  selector: 'app-users-table-pagination',
  templateUrl: './users-table-pagination.component.html',
  styleUrl: './users-table-pagination.component.scss',
})
export class UsersTablePaginationComponent implements OnInit {
  @Output() paginationRefresh: EventEmitter<void> = new EventEmitter();

  public constructor(
    protected readonly paginationService: UsersPaginationService,
    protected readonly dataService: UsersDataService,
  ) {}

  public ngOnInit(): void {
    this.paginationService.refreshPagination();
  }
}
