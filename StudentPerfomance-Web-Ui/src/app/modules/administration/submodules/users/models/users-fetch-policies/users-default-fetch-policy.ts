import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IFetchPolicy } from '../../../../../../shared/models/fetch-policices/fetch-policy-interface';
import { UserRecord } from '../../services/user-table-element-interface';
import { User } from '../../../../../users/services/user-interface';
import { BASE_API_URI } from '../../../../../../shared/models/api/api-constants';

export class DefaultFetchPolicy implements IFetchPolicy<UserRecord[]> {
  private readonly _user: User;
  private readonly _apiUri: string;
  private _httpParams: HttpParams;

  public constructor(user: User) {
    this._user = user;
    this._apiUri = `${BASE_API_URI}/api/users/read/byPage`;
  }

  public executeFetchPolicy(httpClient: HttpClient): Observable<UserRecord[]> {
    const params = this._httpParams;
    return httpClient.get<UserRecord[]>(this._apiUri, { params });
  }

  public addPages(page: number, pageSize: number): void {
    this._httpParams = new HttpParams()
      .set('Page', page)
      .set('PageSize', pageSize)
      .set('Token', this._user.token);
  }
}
