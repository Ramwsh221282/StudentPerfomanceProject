import { inject, Injectable } from '@angular/core';
import { UserRecord } from './user-table-element-interface';
import { IFetchPolicy } from '../../../../../shared/models/fetch-policices/fetch-policy-interface';
import { HttpClient } from '@angular/common/http';
import { User } from '../../../../users/services/user-interface';
import { AuthService } from '../../../../users/services/auth.service';
import { DefaultFetchPolicy } from '../models/users-fetch-policies/users-default-fetch-policy';
import { IObservableFetchable } from '../../../../../shared/models/fetch-policices/iobservable-fetchable.interface';
import { Observable } from 'rxjs';
import { AppConfigService } from '../../../../../app.config.service';

@Injectable({
  providedIn: 'any',
})
export class UsersDataService implements IObservableFetchable<UserRecord[]> {
  private readonly _httpClient: HttpClient;
  private readonly _user: User;
  private _policy: IFetchPolicy<UserRecord[]>;

  public constructor(private readonly _appConfig: AppConfigService) {
    const authService: AuthService = inject(AuthService);
    this._user = authService.userData;
    this._httpClient = inject(HttpClient);
    this._policy = new DefaultFetchPolicy(this._user, this._appConfig);
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
