import { Injectable } from '@angular/core';
import { BaseHttpService } from '../../shared/models/common/base-http/base-http.service';
import { Observable } from 'rxjs';
import { AssignmentSession } from '../../modules/administration/submodules/assignment-sessions/models/assignment-session-interface';

@Injectable({
  providedIn: 'any',
})
export class CurrentControlWeekDataService extends BaseHttpService {
  public getCurrent(): Observable<AssignmentSession> {
    const url = `${this._config.baseApiUri}/api/assignment-sessions`;
    const headers = this.buildHttpHeaders();
    return this._http.get<AssignmentSession>(url, { headers: headers });
  }
}
