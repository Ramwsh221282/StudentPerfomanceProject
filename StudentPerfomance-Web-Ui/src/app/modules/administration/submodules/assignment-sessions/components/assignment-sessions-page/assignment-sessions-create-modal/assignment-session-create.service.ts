import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from '../../../../../../users/services/auth.service';
import { BASE_API_URI } from '../../../../../../../shared/models/api/api-constants';
import { AssignmentSessionDate } from '../../../models/contracts/assignment-session-contract/assignment-session-date';
import { Observable } from 'rxjs';
import { AssignmentSession } from '../../../models/assignment-session-interface';

@Injectable({
  providedIn: 'any',
})
export class AssignmentSessionCreateService {
  private readonly _apiUri: string;

  public constructor(
    private readonly _authService: AuthService,
    private readonly _httpClient: HttpClient,
  ) {
    this._apiUri = `${BASE_API_URI}/api/assignment-sessions`;
  }

  public create(
    startDate: AssignmentSessionDate,
    endDate: AssignmentSessionDate,
  ): Observable<AssignmentSession> {
    const payload = this.buildPayload(startDate, endDate);
    const headers = this.buildHttpHeaders();
    return this._httpClient.post<AssignmentSession>(this._apiUri, payload, {
      headers: headers,
    });
  }

  private buildPayload(
    startDate: AssignmentSessionDate,
    endDate: AssignmentSessionDate,
  ): object {
    return {
      dateStart: {
        day: startDate.day,
        month: startDate.month,
        year: startDate.year,
      },
      dateClose: {
        day: endDate.day,
        month: endDate.month,
        year: endDate.year,
      },
    };
  }

  private buildHttpHeaders(): HttpHeaders {
    return new HttpHeaders().set('token', this._authService.userData.token);
  }
}
