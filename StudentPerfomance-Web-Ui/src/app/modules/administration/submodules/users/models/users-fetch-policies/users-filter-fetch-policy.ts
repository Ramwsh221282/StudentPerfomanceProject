import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IFetchPolicy } from '../../../../../../shared/models/fetch-policices/fetch-policy-interface';
import { UserRecord } from '../../services/user-table-element-interface';
import { User } from '../../../../../users/services/user-interface';
import { BASE_API_URI } from '../../../../../../shared/models/api/api-constants';
import { PaginationPayloadBuilder } from '../../../../../../shared/models/common/pagination-contract/pagination-payload-builder';
import { TokenPayloadBuilder } from '../../../../../../shared/models/common/token-contract/token-payload-builder';
import { AuthService } from '../../../../../users/services/auth.service';

export class UsersFilterFetchPolicy implements IFetchPolicy<UserRecord[]> {
  private readonly _user: User;
  private readonly _apiUri: string;
  private _payload: object;

  public constructor(
    user: User,
    private readonly _authService: AuthService,
  ) {
    this._user = { ...user };
    this._apiUri = `${BASE_API_URI}/api/users/filter`;
  }

  public executeFetchPolicy(httpClient: HttpClient): Observable<UserRecord[]> {
    const payload = this._payload;
    return httpClient.post<UserRecord[]>(this._apiUri, payload);
  }

  public addPages(page: number, pageSize: number): void {
    this._payload = {
      user: {
        name: this._user.name,
        surname: this._user.surname,
        patronymic: this._user.patronymic,
        email: this._user.email,
        role: this._user.role,
      },
      pagination: PaginationPayloadBuilder(page, pageSize),
      token: TokenPayloadBuilder(this._authService.userData),
    };
  }
}
