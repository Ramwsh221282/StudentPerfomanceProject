import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IObservableFetchable } from '../../../../../../../shared/models/fetch-policices/iobservable-fetchable.interface';
import { AssignmentSession } from '../../../models/assignment-session-interface';
import { Observable } from 'rxjs';
import { IFetchPolicy } from '../../../../../../../shared/models/fetch-policices/fetch-policy-interface';
import { ControlWeekReportEntity } from '../../../models/report/control-week-report';
import { BASE_API_URI } from '../../../../../../../shared/models/api/api-constants';
import { TokenPayloadBuilder } from '../../../../../../../shared/models/common/token-contract/token-payload-builder';
import { AuthService } from '../../../../../../users/services/auth.service';

@Injectable({
  providedIn: 'any',
})
export class AssignmentSessionDataService
  implements IObservableFetchable<AssignmentSession[]>
{
  private _policy: IFetchPolicy<AssignmentSession[]>;

  public constructor(
    private readonly _httpClient: HttpClient,
    private readonly _authService: AuthService,
  ) {}

  public setPolicy(policy: IFetchPolicy<AssignmentSession[]>): void {
    this._policy = policy;
  }

  public fetch(): Observable<AssignmentSession[]> {
    return this._policy.executeFetchPolicy(this._httpClient);
  }

  public getReport(
    session: AssignmentSession,
  ): Observable<ControlWeekReportEntity | null> {
    const apiUri: string = `${BASE_API_URI}/api/assignment-sessions/report`;
    const payload: object = {
      token: TokenPayloadBuilder(this._authService.userData),
      id: session.id,
    };
    return this._httpClient.post<ControlWeekReportEntity | null>(
      apiUri,
      payload,
    );
  }
}
