import { IFetchPolicy } from '../../../../../models/fetch-policices/fetch-policy-interface';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from '../../../../../../modules/users/services/auth.service';
import { BASE_API_URI } from '../../../../../models/api/api-constants';
import { TokenPayloadBuilder } from '../../../../../models/common/token-contract/token-payload-builder';
import { PaginationPayloadBuilder } from '../../../../../models/common/pagination-contract/pagination-payload-builder';
import { ControlWeekReportInterface } from '../../../Models/Data/control-week-report-interface';

export class SessionReportDefaultFetchPolicy
  implements IFetchPolicy<ControlWeekReportInterface[]>
{
  private readonly _baseApiUri: string;
  private _payload: object;

  public constructor(private readonly _authService: AuthService) {
    this._baseApiUri = `${BASE_API_URI}/app/assignment-sessions/reports-paged`;
  }

  public executeFetchPolicy(
    httpClient: HttpClient,
  ): Observable<ControlWeekReportInterface[]> {
    const payload = this._payload;
    return httpClient.post<ControlWeekReportInterface[]>(
      this._baseApiUri,
      payload,
    );
  }

  public addPages(page: number, pageSize: number): void {
    this.buildPayload(page, pageSize);
  }

  private buildPayload(page: number, pageSize: number) {
    this._payload = {
      token: TokenPayloadBuilder(this._authService.userData),
      pagination: PaginationPayloadBuilder(page, pageSize),
    };
  }
}
