import { Component, EventEmitter, Input, Output } from '@angular/core';
import { UserRecord } from '../../services/user-table-element-interface';
import { Router } from '@angular/router';
import { User } from '../../../../../users/services/user-interface';

@Component({
  selector: 'app-users-table',
  templateUrl: './users-table.component.html',
  styleUrl: './users-table.component.scss',
})
export class UsersTableComponent {
  @Input({ required: true }) users: UserRecord[];
  @Output() refreshOnUserCreated: EventEmitter<void> = new EventEmitter();
  @Output() filtered: EventEmitter<void> = new EventEmitter();
  @Output() removed: EventEmitter<void> = new EventEmitter();

  protected currentlySelectedUser: UserRecord | null = null;
  protected isCreatingAdmin: boolean = false;

  protected isCreatingTeacher: boolean = false;

  protected isFilteringUsers: boolean = false;

  protected isDeletingUser: boolean = false;
  protected userToRemove: UserRecord | null = null;

  public constructor(private readonly _router: Router) {}

  protected navigateToDocumentation(): void {
    const path = ['/users-info'];
    this._router.navigate(path);
  }

  protected manageCurrentlySelectedUser(user: UserRecord): boolean {
    if (!this.currentlySelectedUser) return false;
    return user == this.currentlySelectedUser;
  }

  protected handleUserCreated(user: User): void {
    this.refreshOnUserCreated.emit();
  }
}
