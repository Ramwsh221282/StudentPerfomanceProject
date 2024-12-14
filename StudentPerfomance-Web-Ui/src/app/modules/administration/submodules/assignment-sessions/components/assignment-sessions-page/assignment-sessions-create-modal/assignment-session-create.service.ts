import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from '../../../../../../users/services/auth.service';
//import { BASE_API_URI } from '../../../../../../../shared/models/api/api-constants';
import { AssignmentSessionDate } from '../../../models/contracts/assignment-session-contract/assignment-session-date';
import { Observable } from 'rxjs';
import { AssignmentSession } from '../../../models/assignment-session-interface';
import { AppConfigService } from '../../../../../../../app.config.service';

@Injectable({
  providedIn: 'any',
})
export class AssignmentSessionCreateService {
  private readonly _apiUri: string;

  public constructor(
    private readonly _authService: AuthService,
    private readonly _httpClient: HttpClient,
    private readonly _appConfig: AppConfigService,
  ) {
    //this._apiUri = `${BASE_API_URI}/api/assignment-sessions`;
    this._apiUri = `${this._appConfig.baseApiUri}/api/assignment-sessions`;
  }

  public create(
    startDate: AssignmentSessionDate,
    season: string,
    number: number,
  ): Observable<AssignmentSession> {
    const payload = this.buildPayload(startDate, season, number);
    const headers = this.buildHttpHeaders();
    return this._httpClient.post<AssignmentSession>(this._apiUri, payload, {
      headers: headers,
    });
  }

  private buildPayload(
    startDate: AssignmentSessionDate,
    season: string,
    number: number,
  ): object {
    return {
      dateStart: {
        day: startDate.day,
        month: startDate.month,
        year: startDate.year,
      },
      season: season,
      number: number,
    };
  }

  private buildHttpHeaders(): HttpHeaders {
    return new HttpHeaders().set('token', this._authService.userData.token);
  }
}
