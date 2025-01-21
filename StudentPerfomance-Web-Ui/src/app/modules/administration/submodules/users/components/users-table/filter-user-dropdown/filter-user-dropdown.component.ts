import { Component, EventEmitter, Input, Output } from '@angular/core';
import { UsersDataService } from '../../../services/users-data.service';
import { UsersPaginationService } from '../users-table-pagination/users-pagination.servce';
import { AuthService } from '../../../../../../../pages/user-page/services/auth.service';
import { AppConfigService } from '../../../../../../../app.config.service';
import { User } from '../../../../../../../pages/user-page/services/user-interface';
import { UsersFilterFetchPolicy } from '../../../models/users-fetch-policies/users-filter-fetch-policy';
import { DefaultFetchPolicy } from '../../../models/users-fetch-policies/users-default-fetch-policy';

@Component({
    selector: 'app-filter-user-dropdown',
    templateUrl: './filter-user-dropdown.component.html',
    styleUrl: './filter-user-dropdown.component.scss',
    standalone: false
})
export class FilterUserDropdownComponent {
  @Input({ required: true }) visibility: boolean = false;
  @Output() visibilityChange: EventEmitter<boolean> = new EventEmitter();
  @Output() filtered: EventEmitter<void> = new EventEmitter();

  protected name: string = '';
  protected surname: string = '';
  protected patronymic: string = '';
  protected role: string = '';
  protected email: string = '';
  protected roles: string[] = ['Администратор', 'Преподаватель'];
  protected isSelectingRole: boolean = false;

  public constructor(
    private readonly _dataService: UsersDataService,
    private readonly _paginationService: UsersPaginationService,
    private readonly _authService: AuthService,
    private readonly _appConfig: AppConfigService,
  ) {}

  public submit(): void {
    const user = this.createUser();
    this._dataService.setPolicy(
      new UsersFilterFetchPolicy(user, this._authService, this._appConfig),
    );
    this._dataService.addPages(
      this._paginationService.currentPage,
      this._paginationService.pageSize,
    );
    this.filtered.emit();
    this.close();
  }

  protected cancel(): void {
    this._dataService.setPolicy(
      new DefaultFetchPolicy(this._authService.userData, this._appConfig),
    );
    this._dataService.addPages(
      this._paginationService.currentPage,
      this._paginationService.pageSize,
    );
    this.filtered.emit();
    this.close();
  }

  protected close(): void {
    this.visibility = false;
    this.visibilityChange.emit(this.visibility);
  }

  private createUser(): User {
    const user = {} as User;
    user.name = this.name;
    user.surname = this.surname;
    user.patronymic = this.patronymic;
    user.email = this.email;
    user.role = this.role;
    return user;
  }
}
