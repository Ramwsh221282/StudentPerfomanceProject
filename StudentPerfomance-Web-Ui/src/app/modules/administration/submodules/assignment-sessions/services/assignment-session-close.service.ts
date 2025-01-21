import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthService } from '../../../../../pages/user-page/services/auth.service';
//import { BASE_API_URI } from '../../../../../../../../../shared/models/api/api-constants';
import { AssignmentSession } from '../models/assignment-session-interface';
import { Observable } from 'rxjs';
import { TokenPayloadBuilder } from '../../../../../shared/models/common/token-contract/token-payload-builder';
import { AppConfigService } from '../../../../../app.config.service';

@Injectable({
  providedIn: 'any',
})
export class AssignmentSessionCloseService {
  private readonly _apiUri: string;

  public constructor(
    private readonly _httpClient: HttpClient,
    private readonly _authService: AuthService,
    private readonly _appConfig: AppConfigService,
  ) {
    //this._apiUri = `${BASE_API_URI}/api/assignment-sessions/close-session`;
    this._apiUri = `${this._appConfig.baseApiUri}/api/assignment-sessions/close-session`;
  }

  public close(session: AssignmentSession): Observable<any> {
    const payload = this.buildPayload(session);
    const headers = this.buildHttpHeaders();
    return this._httpClient.put(this._apiUri, payload, { headers: headers });
  }

  private buildPayload(session: AssignmentSession): object {
    return {
      token: TokenPayloadBuilder(this._authService.userData),
      id: session.id,
    };
  }

  private buildHttpHeaders(): HttpHeaders {
    return new HttpHeaders().set('token', this._authService.userData.token);
  }
}
