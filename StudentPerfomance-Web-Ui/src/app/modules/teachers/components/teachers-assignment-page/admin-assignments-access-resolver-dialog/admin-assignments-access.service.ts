import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { AuthService } from '../../../../../pages/user-page/services/auth.service';
import { Observable } from 'rxjs';
import { AppConfigService } from '../../../../../app.config.service';

@Injectable({
  providedIn: 'any',
})
export class AdminAssignmentsAccessService {
  constructor(
    private readonly _httpClient: HttpClient,
    private readonly _authService: AuthService,
    private readonly _configService: AppConfigService,
  ) {}

  public tryGetAccess(teacherId: string): Observable<AdminAccessResponse> {
    const token = this._authService.userData.token;
    const headers = this.buildHttpHeaders(token);
    const params = this.buildHttpParams(teacherId);
    const apiUrl = `${this._configService.baseApiUri}/app/assignment-sessions/admin-assignments-access`;
    return this._httpClient.get<AdminAccessResponse>(apiUrl, {
      headers,
      params,
    });
  }

  private buildHttpHeaders(token: string): HttpHeaders {
    return new HttpHeaders().set('token', token);
  }

  private buildHttpParams(teacherId: string): HttpParams {
    return new HttpParams().set('teacherId', teacherId);
  }
}

export interface AdminAccessResponse {
  adminId: string;
  teacherId: string;
}
