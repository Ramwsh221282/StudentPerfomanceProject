import { Observable } from 'rxjs';
import { IFetchPolicy } from '../../../../models/fetch-policices/fetch-policy-interface';
import { IObservableFetchable } from '../../../../models/fetch-policices/iobservable-fetchable.interface';
import { ControlWeekReportInterface } from '../../Models/Data/control-week-report-interface';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BASE_API_URI } from '../../../../models/api/api-constants';
import { AuthService } from '../../../../../modules/users/services/auth.service';
import { TokenPayloadBuilder } from '../../../../models/common/token-contract/token-payload-builder';
import { GroupReportInterface } from '../../Models/Data/group-report-interface';

@Injectable({
  providedIn: 'any',
})
export class SessionReportsDataService
  implements IObservableFetchable<ControlWeekReportInterface[]>
{
  private _policy: IFetchPolicy<ControlWeekReportInterface[]>;

  public constructor(
    private readonly _httpClient: HttpClient,
    private readonly _authService: AuthService,
  ) {}

  public setPolicy(policy: IFetchPolicy<ControlWeekReportInterface[]>): void {
    this._policy = policy;
  }

  public addPages(page: number, pageSize: number): void {
    this._policy.addPages(page, pageSize);
  }

  public fetch(): Observable<ControlWeekReportInterface[]> {
    if (this._policy == undefined)
      return new Observable<ControlWeekReportInterface[]>();
    return this._policy.executeFetchPolicy(this._httpClient);
  }

  public getById(id: string): Observable<GroupReportInterface[]> {
    const apiUri: string = `${BASE_API_URI}/app/assignment-sessions/group-report-by-id`;
    const payload: object = {
      token: TokenPayloadBuilder(this._authService.userData),
      id: id,
    };
    return this._httpClient.post<GroupReportInterface[]>(apiUri, payload);
  }

  public getCourseReportById(
    reportId: string,
  ): Observable<ControlWeekReportInterface> {
    const apiUri: string = `${BASE_API_URI}/app/assignment-sessions/course-report-by-id`;
    const payload: object = {
      token: TokenPayloadBuilder(this._authService.userData),
      id: reportId,
    };
    return this._httpClient.post<ControlWeekReportInterface>(apiUri, payload);
  }
}
