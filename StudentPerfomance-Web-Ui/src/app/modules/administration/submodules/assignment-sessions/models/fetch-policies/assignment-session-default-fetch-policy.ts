import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IFetchPolicy } from '../../../../../../shared/models/fetch-policices/fetch-policy-interface';
import { AssignmentSession } from '../assignment-session-interface';
import { AuthService } from '../../../../../users/services/auth.service';
import { BASE_API_URI } from '../../../../../../shared/models/api/api-constants';
import { PaginationPayloadBuilder } from '../../../../../../shared/models/common/pagination-contract/pagination-payload-builder';
import { TokenPayloadBuilder } from '../../../../../../shared/models/common/token-contract/token-payload-builder';

export class AssignmentSessionDefaultFetchPolicy
  implements IFetchPolicy<AssignmentSession[]>
{
  private readonly _apiUri: string;
  private _payload: object;

  public constructor(private readonly _authService: AuthService) {
    this._apiUri = `${BASE_API_URI}/api/assignment-sessions/byPage`;
  }

  public executeFetchPolicy(
    httpClient: HttpClient
  ): Observable<AssignmentSession[]> {
    const payload = this._payload;
    return httpClient.post<AssignmentSession[]>(this._apiUri, payload);
  }

  public addPages(page: number, pageSize: number): void {
    this.buildPayload(page, pageSize);
  }

  private buildPayload(page: number, pageSize: number): void {
    this._payload = {
      pagination: PaginationPayloadBuilder(page, pageSize),
      token: TokenPayloadBuilder(this._authService.userData),
    };
  }
}
