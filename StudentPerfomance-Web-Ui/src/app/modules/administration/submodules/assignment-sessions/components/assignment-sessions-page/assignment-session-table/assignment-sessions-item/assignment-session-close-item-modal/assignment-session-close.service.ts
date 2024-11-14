import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthService } from '../../../../../../../../users/services/auth.service';
import { BASE_API_URI } from '../../../../../../../../../shared/models/api/api-constants';
import { AssignmentSession } from '../../../../../models/assignment-session-interface';
import { Observable } from 'rxjs';
import { TokenPayloadBuilder } from '../../../../../../../../../shared/models/common/token-contract/token-payload-builder';

@Injectable({
  providedIn: 'any',
})
export class AssignmentSessionCloseService {
  private readonly _apiUri: string;

  public constructor(
    private readonly _httpClient: HttpClient,
    private readonly _authService: AuthService,
  ) {
    this._apiUri = `${BASE_API_URI}/api/assignment-sessions/close-session`;
  }

  public close(session: AssignmentSession): Observable<any> {
    const payload = this.buildPayload(session);
    return this._httpClient.post(this._apiUri, payload);
  }

  private buildPayload(session: AssignmentSession): object {
    return {
      token: TokenPayloadBuilder(this._authService.userData),
      id: session.id,
    };
  }
}
