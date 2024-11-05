import { Injectable } from '@angular/core';
import { AuthService } from '../../../../users/services/auth.service';
import { HttpClient } from '@angular/common/http';
import { BASE_API_URI } from '../../../../../shared/models/api/api-constants';
import { Observable } from 'rxjs';
import { AssignmentSessionInfo } from './assignment-session-into';
import { TokenPayloadBuilder } from '../../../../../shared/models/common/token-contract/token-payload-builder';

@Injectable({
  providedIn: 'any',
})
export class TeacherAssignmentInfoSessionService {
  private readonly _apiUri: string;

  public constructor(
    private readonly _authService: AuthService,
    private readonly _httpClient: HttpClient
  ) {
    this._apiUri = `${BASE_API_URI}/app/assignment-sessions/active-session-info`;
  }

  public getInfo(): Observable<AssignmentSessionInfo> {
    const payload = {
      token: TokenPayloadBuilder(this._authService.userData),
    };
    return this._httpClient.post<AssignmentSessionInfo>(this._apiUri, payload);
  }
}
