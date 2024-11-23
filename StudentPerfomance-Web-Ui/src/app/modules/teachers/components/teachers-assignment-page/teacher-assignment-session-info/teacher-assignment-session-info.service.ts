import { Injectable } from '@angular/core';
import { AuthService } from '../../../../users/services/auth.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BASE_API_URI } from '../../../../../shared/models/api/api-constants';
import { Observable } from 'rxjs';
import { AssignmentSessionInfo } from './assignment-session-into';

@Injectable({
  providedIn: 'any',
})
export class TeacherAssignmentInfoSessionService {
  private readonly _apiUri: string;

  public constructor(
    private readonly _authService: AuthService,
    private readonly _httpClient: HttpClient,
  ) {
    this._apiUri = `${BASE_API_URI}/app/assignment-sessions/active-session-info`;
  }

  public getInfo(): Observable<AssignmentSessionInfo> {
    const headers = this.buildHttpHeaders();
    return this._httpClient.get<AssignmentSessionInfo>(this._apiUri, {
      headers: headers,
    });
  }

  private buildHttpHeaders(): HttpHeaders {
    return new HttpHeaders().set('token', this._authService.userData.token);
  }
}
