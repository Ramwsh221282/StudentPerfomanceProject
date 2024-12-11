import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { User } from '../../../../../users/services/user-interface';
import { AuthService } from '../../../../../users/services/auth.service';
//import { BASE_API_URI } from '../../../../../../shared/models/api/api-constants';
import { UserRecord } from '../../services/user-table-element-interface';
import { Observable } from 'rxjs';
import { AppConfigService } from '../../../../../../app.config.service';

@Injectable({
  providedIn: 'any',
})
export class UserRemoveService {
  private readonly _httpClient: HttpClient;
  private readonly _user: User;
  private readonly _apiUri: string;

  public constructor(private readonly _appConfig: AppConfigService) {
    this._httpClient = inject(HttpClient);
    const authService = inject(AuthService);
    this._user = { ...authService.userData };
    //this._apiUri = `${BASE_API_URI}/api/users`;
    this._apiUri = `${_appConfig.baseApiUri}/api/users`;
  }

  public remove(user: UserRecord): Observable<UserRecord> {
    const body = this.buildPayload(user);
    const headers = this.buildHttpHeaders();
    return this._httpClient.delete<UserRecord>(this._apiUri, {
      headers: headers,
      body,
    });
  }

  private buildPayload(user: UserRecord): object {
    return {
      user: {
        name: user.name,
        surname: user.surname,
        thirdname: user.patronymic,
        role: user.role,
        email: user.email,
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
