import { Observable } from 'rxjs';
import { IFetchPolicy } from '../../../../models/fetch-policices/fetch-policy-interface';
import { IObservableFetchable } from '../../../../models/fetch-policices/iobservable-fetchable.interface';
import { ControlWeekReportInterface } from '../../Models/Data/control-week-report-interface';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
//import { BASE_API_URI } from '../../../../models/api/api-constants';
import { AuthService } from '../../../../../pages/user-page/services/auth.service';
import { GroupReportInterface } from '../../Models/Data/group-report-interface';
import { AppConfigService } from '../../../../../app.config.service';

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
    private readonly _appconfig: AppConfigService,
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
    //const apiUri: string = `${BASE_API_URI}/app/assignment-sessions/group-report-by-id`;
    const apiUri: string = `${this._appconfig.baseApiUri}/app/assignment-sessions/group-report-by-id`;
    const headers = this.buildHttpHeaders();
    const params = this.buildHttpParams(id);
    return this._httpClient.get<GroupReportInterface[]>(apiUri, {
      headers: headers,
      params,
    });
  }

  public getCourseReportById(
    reportId: string,
  ): Observable<ControlWeekReportInterface> {
    //const apiUri: string = `${BASE_API_URI}/app/assignment-sessions/course-report-by-id`;
    const apiUri: string = `${this._appconfig.baseApiUri}/app/assignment-sessions/course-report-by-id`;
    const headers = this.buildHttpHeaders();
    const params = this.buildHttpParams(reportId);
    return this._httpClient.get<ControlWeekReportInterface>(apiUri, {
      headers: headers,
      params,
    });
  }

  private buildHttpParams(id: string): HttpParams {
    return new HttpParams().set('id', id);
  }

  private buildHttpHeaders(): HttpHeaders {
    return new HttpHeaders().set('token', this._authService.userData.token);
  }
}
