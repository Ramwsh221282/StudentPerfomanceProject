import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthService } from '../../../../pages/user-page/services/auth.service';
import { AppConfigService } from '../../../../app.config.service';
import { inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class BaseHttpService {
  protected readonly _http: HttpClient;
  protected readonly _auth: AuthService;
  protected readonly _config: AppConfigService;

  public constructor() {
    this._http = inject(HttpClient);
    this._auth = inject(AuthService);
    this._config = inject(AppConfigService);
  }

  public buildHttpHeaders(): HttpHeaders {
    return new HttpHeaders().set('token', this._auth.userData.token);
  }
}
