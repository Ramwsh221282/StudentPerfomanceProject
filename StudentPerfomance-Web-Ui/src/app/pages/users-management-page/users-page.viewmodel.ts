import { Injectable } from '@angular/core';
import { UserRecord } from '../../modules/administration/submodules/users/services/user-table-element-interface';

@Injectable({
  providedIn: 'any',
})
export class UsersPageViewmodel {
  private _users: UserRecord[] = [];
  private _isInited: boolean = false;
  private _currentUser: UserRecord | null = null;
  public isCreatingNew: boolean = false;
  public selectedRole: string = '';

  public get currentUser(): UserRecord | null {
    return this._currentUser;
  }

  public isSelected(user: UserRecord): boolean {
    if (!this._currentUser) return false;
    return this._currentUser.id == user.id;
  }

  public select(user: UserRecord): void {
    this._currentUser = user;
  }

  public get users(): UserRecord[] {
    return this._users;
  }

  public initialize(users: UserRecord[]): void {
    if (this._isInited) return;
    this._users = users;
  }

  public addUser(user: UserRecord): void {
    this._users.push(user);
  }

  public removeUser(user: UserRecord): void {
    const index = this._users.findIndex((u) => u.id == user.id);
    if (index < 0) return;
    if (
      this._currentUser != null &&
      this._users[index].id == this._currentUser.id
    )
      this._currentUser = null;
    this._users.splice(index, 1);
  }
}
