import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject } from '@angular/core';
//import { BASE_API_URI } from '../../../../../shared/models/api/api-constants';
import { User } from '../../../../../pages/user-page/services/user-interface';
import { AuthService } from '../../../../../pages/user-page/services/auth.service';
import { Observable } from 'rxjs';
import { UserRecord } from './user-table-element-interface';
import { AppConfigService } from '../../../../../app.config.service';

export class UserCreationService {
  private readonly _httpClient: HttpClient;
  private readonly _apiUri: string;
  private readonly _user: User;
  private readonly _appConfig: AppConfigService;

  public constructor() {
    this._appConfig = inject(AppConfigService);
    this._httpClient = inject(HttpClient);
    //this._apiUri = `${BASE_API_URI}/api/users`;
    this._apiUri = `${this._appConfig.baseApiUri}/api/users`;
    const authService = inject(AuthService);
    this._user = { ...authService.userData };
  }

  public create(user: User): Observable<UserRecord> {
    const headers = this.buildHttpHeaders();
    const payload = this.buildPayload(user);
    return this._httpClient.post<UserRecord>(this._apiUri, payload, {
      headers: headers,
    });
  }

  private buildPayload(user: User): object {
    return {
      user: {
        email: user.email,
        name: user.name,
        surname: user.surname,
        patronymic: user.patronymic,
        role: user.role,
      },
      token: {
        token: this._user.token,
      },
    };
  }

  private buildHttpHeaders(): HttpHeaders {
    return new HttpHeaders().set('token', this._user.token);
  }
}
