import { Injectable } from '@angular/core';
import { BaseHttpService } from '../../../shared/models/common/base-http/base-http.service';
import { AssignmentSessionDate } from '../../../modules/administration/submodules/assignment-sessions/models/contracts/assignment-session-contract/assignment-session-date';
import { Observable } from 'rxjs';
import { AssignmentSession } from '../../../modules/administration/submodules/assignment-sessions/models/assignment-session-interface';

@Injectable({
  providedIn: 'any',
})
export class CreateControlWeekService extends BaseHttpService {
  public create(
    startDate: AssignmentSessionDate,
    season: string,
    number: number,
  ): Observable<AssignmentSession> {
    const payload = this.buildPayload(startDate, season, number);
    const headers = this.buildHttpHeaders();
    const url = `${this._config.baseApiUri}/api/assignment-sessions`;
    return this._http.post<AssignmentSession>(url, payload, {
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
}
