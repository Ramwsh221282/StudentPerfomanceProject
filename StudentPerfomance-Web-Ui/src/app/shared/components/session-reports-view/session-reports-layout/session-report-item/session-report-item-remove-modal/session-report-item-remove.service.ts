import { Injectable } from '@angular/core';
import { AppConfigService } from '../../../../../../app.config.service';
import { AuthService } from '../../../../../../pages/user-page/services/auth.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ControlWeekReportInterface } from '../../../Models/Data/control-week-report-interface';

@Injectable({
  providedIn: 'any',
})
export class SessionReportItemRemoveService {
  private readonly _apiUri: string;

  public constructor(
    private readonly _appConfig: AppConfigService,
    private readonly _authService: AuthService,
    private readonly _httpClient: HttpClient,
  ) {
    this._apiUri = `${this._appConfig.baseApiUri}/api/assignment-sessions/remove-report`;
  }

  public removeReport(report: ControlWeekReportInterface): Observable<any> {
    const body = this.buildPayload(report);
    const headers = this.buildHttpHeaders();
    return this._httpClient.delete(this._apiUri, { headers: headers, body });
  }

  private buildPayload(report: ControlWeekReportInterface): object {
    return {
      id: report.id,
    };
  }

  private buildHttpHeaders(): HttpHeaders {
    return new HttpHeaders().set('token', this._authService.userData.token);
  }
}
