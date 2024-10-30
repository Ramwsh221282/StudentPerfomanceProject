import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IFetchPolicy } from '../../../../../../shared/models/fetch-policices/fetch-policy-interface';
import { Department } from '../departments.interface';
import { BASE_API_URI } from '../../../../../../shared/models/api/api-constants';
import { AuthService } from '../../../../../users/services/auth.service';
import { User } from '../../../../../users/services/user-interface';
import { PaginationPayloadBuilder } from '../../../../../../shared/models/common/pagination-contract/pagination-payload-builder';
import { TokenPayloadBuilder } from '../../../../../../shared/models/common/token-contract/token-payload-builder';

export class DepartmentDefaultFetchPolicy
  implements IFetchPolicy<Department[]>
{
  private readonly _apiUri: string;
  private readonly _user: User;
  private _payload: object;

  public constructor(authService: AuthService) {
    this._apiUri = `${BASE_API_URI}/api/teacher-departments/byPage`;
    this._user = { ...authService.userData };
    this._payload = {
      pagination: {
        page: 0,
        pageSize: 0,
      },
      token: {
        token: this._user.token,
      },
    };
  }

  public executeFetchPolicy(httpClient: HttpClient): Observable<Department[]> {
    const payload = this._payload;
    return httpClient.post<Department[]>(this._apiUri, payload);
  }

  public addPages(page: number, pageSize: number): void {
    this._payload = {
      pagination: PaginationPayloadBuilder(page, pageSize),
      token: TokenPayloadBuilder(this._user),
    };
  }
}
