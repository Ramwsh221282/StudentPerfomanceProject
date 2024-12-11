import { Injectable } from '@angular/core';
import { AuthService } from '../../../../users/services/auth.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AssignmentSessionInfo } from './assignment-session-into';
import { AppConfigService } from '../../../../../app.config.service';

@Injectable({
  providedIn: 'any',
})
export class TeacherAssignmentInfoSessionService {
  private readonly _apiUri: string;

  public constructor(
    private readonly _authService: AuthService,
    private readonly _httpClient: HttpClient,
    private readonly _configService: AppConfigService,
  ) {
    //this._apiUri = `${BASE_API_URI}/app/assignment-sessions/active-session-info`;
    this._apiUri = `${this._configService.baseApiUri}/app/assignment-sessions/active-session-info`;
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
