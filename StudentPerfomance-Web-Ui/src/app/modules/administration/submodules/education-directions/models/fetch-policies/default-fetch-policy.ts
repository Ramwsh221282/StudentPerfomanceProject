import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IFetchPolicy } from '../../../../../../shared/models/fetch-policices/fetch-policy-interface';
import { EducationDirection } from '../education-direction-interface';
import { BASE_API_URI } from '../../../../../../shared/models/api/api-constants';
import { User } from '../../../../../users/services/user-interface';
import { AuthService } from '../../../../../users/services/auth.service';
import { PaginationPayloadBuilder } from '../../../../../../shared/models/common/pagination-contract/pagination-payload-builder';
import { TokenPayloadBuilder } from '../../../../../../shared/models/common/token-contract/token-payload-builder';

export class DefaultFetchPolicy implements IFetchPolicy<EducationDirection[]> {
  private readonly _baseApiUri = `${BASE_API_URI}/api/education-direction/byPage`;
  private _user: User;
  private _payload: object;

  public constructor(private readonly authService: AuthService) {
    this._user = { ...authService.userData };
  }

  public executeFetchPolicy(
    httpClient: HttpClient
  ): Observable<EducationDirection[]> {
    const payload = this._payload;
    return httpClient.post<EducationDirection[]>(this._baseApiUri, payload);
  }

  public addPages(page: number, pageSize: number): void {
    this._payload = {
      pagination: PaginationPayloadBuilder(page, pageSize),
      token: TokenPayloadBuilder(this._user),
    };
  }
}
