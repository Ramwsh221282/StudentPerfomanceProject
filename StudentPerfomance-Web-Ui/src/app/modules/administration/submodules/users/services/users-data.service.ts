import { inject, Injectable } from '@angular/core';
import { IFetchable } from '../../../../../shared/models/fetch-policices/ifetchable-interface';
import { UserRecord } from './user-table-element-interface';
import { IFetchPolicy } from '../../../../../shared/models/fetch-policices/fetch-policy-interface';
import { HttpClient } from '@angular/common/http';
import { User } from '../../../../users/services/user-interface';
import { AuthService } from '../../../../users/services/auth.service';
import { DefaultFetchPolicy } from '../models/users-fetch-policies/users-default-fetch-policy';
import { IObservableFetchable } from '../../../../../shared/models/fetch-policices/iobservable-fetchable.interface';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'any',
})
export class UsersDataService implements IObservableFetchable<UserRecord[]> {
  private readonly _httpClient: HttpClient;
  private readonly _user: User;

  private _policy: IFetchPolicy<UserRecord[]>;
  private _userRecords: UserRecord[];

  public constructor() {
    const authService: AuthService = inject(AuthService);
    this._user = authService.userData;
    this._httpClient = inject(HttpClient);
    this._userRecords = [];
    this._policy = new DefaultFetchPolicy(this._user);
    this._policy.addPages(1, 10);
  }

  public fetch(): Observable<UserRecord[]> {
    return this._policy.executeFetchPolicy(this._httpClient);
  }

  public setPolicy(policy: IFetchPolicy<UserRecord[]>): void {
    this._policy = policy;
  }

  public addPages(page: number, pageSize: number): void {
    this._policy.addPages(page, pageSize);
  }
}
