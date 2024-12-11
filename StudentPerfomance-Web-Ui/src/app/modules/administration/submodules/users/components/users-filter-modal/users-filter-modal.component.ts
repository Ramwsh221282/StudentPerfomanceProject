import { Component, EventEmitter, Output } from '@angular/core';
import { User } from '../../../../../users/services/user-interface';
import { ISubbmittable } from '../../../../../../shared/models/interfaces/isubbmitable';
import { UsersDataService } from '../../services/users-data.service';
import { UsersPaginationService } from '../users-table/users-table-pagination/users-pagination.servce';
import { AuthService } from '../../../../../users/services/auth.service';
import { UsersFilterFetchPolicy } from '../../models/users-fetch-policies/users-filter-fetch-policy';
import { DefaultFetchPolicy } from '../../models/users-fetch-policies/users-default-fetch-policy';
import { AppConfigService } from '../../../../../../app.config.service';

@Component({
  selector: 'app-users-filter-modal',
  templateUrl: './users-filter-modal.component.html',
  styleUrl: './users-filter-modal.component.scss',
})
export class UsersFilterModalComponent implements ISubbmittable {
  @Output() visibility: EventEmitter<void> = new EventEmitter();
  @Output() filteredEmitter: EventEmitter<void> = new EventEmitter();
  protected readonly user: User;

  public constructor(
    private readonly _dataService: UsersDataService,
    private readonly _paginationService: UsersPaginationService,
    private readonly _authService: AuthService,
    private readonly _appConfig: AppConfigService,
  ) {
    this.user = {} as User;
    this.user.name = '';
    this.user.surname = '';
    this.user.patronymic = '';
    this.user.email = '';
    this.user.role = '';
  }

  public submit(): void {
    this._dataService.setPolicy(
      new UsersFilterFetchPolicy(this.user, this._appConfig),
    );
    this._dataService.addPages(
      this._paginationService.currentPage,
      this._paginationService.pageSize,
    );
    this.filteredEmitter.emit();
    this.visibility.emit();
  }

  protected cancelFilter(): void {
    this._dataService.setPolicy(
      new DefaultFetchPolicy(this._authService.userData, this._appConfig),
    );
    this._dataService.addPages(
      this._paginationService.currentPage,
      this._paginationService.pageSize,
    );
    this.filteredEmitter.emit();
    this.visibility.emit();
  }
}
