import { Injectable } from '@angular/core';
import { BaseHttpService } from '../../shared/models/common/base-http/base-http.service';
import { AssignmentSession } from '../../modules/administration/submodules/assignment-sessions/models/assignment-session-interface';
import { TokenPayloadBuilder } from '../../shared/models/common/token-contract/token-payload-builder';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'any',
})
export class CloseControlWeekService extends BaseHttpService {
  public close(session: AssignmentSession): Observable<any> {
    const url = `${this._config.baseApiUri}/api/assignment-sessions/close-session`;
    const headers = this.buildHttpHeaders();
    const payload = this.buildPayload(session);
    return this._http.put(url, payload, { headers: headers });
  }

  private buildPayload(session: AssignmentSession): object {
    return {
      token: TokenPayloadBuilder(this._auth.userData),
      id: session.id,
    };
  }
}
