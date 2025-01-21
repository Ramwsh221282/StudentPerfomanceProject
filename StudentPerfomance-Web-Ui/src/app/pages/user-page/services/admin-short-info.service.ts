import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthService } from './auth.service';
import { AppConfigService } from '../../../app.config.service';
import { IAdminShortInfo } from '../models/admin-short-info.interface';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'any',
})
export class AdminShortInfoService {
  constructor(
    private readonly _httpClient: HttpClient,
    private readonly _authService: AuthService,
    private readonly _appConfigService: AppConfigService,
  ) {}

  public invokeGetInfo(): Observable<IAdminShortInfo> {
    const headers = this.buildHttpHeaders();
    return this._httpClient.get<IAdminShortInfo>(
      `${this._appConfigService.baseApiUri}/api/users/admin-info`,
      {
        headers: headers,
      },
    );
  }

  private buildHttpHeaders(): HttpHeaders {
    return new HttpHeaders().set('token', this._authService.userData.token);
  }
}
