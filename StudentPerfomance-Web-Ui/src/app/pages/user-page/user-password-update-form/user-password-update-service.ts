import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthService } from '../services/auth.service';
import { Observable } from 'rxjs';
import { User } from '../services/user-interface';
import { AppConfigService } from '../../../app.config.service';

//import { BASE_API_URI } from '../../../models/api/api-constants';

@Injectable({
  providedIn: 'any',
})
export class UserPasswordUpdateService {
  public constructor(
    private readonly _httpClient: HttpClient,
    private readonly _authService: AuthService,
    private readonly _configService: AppConfigService,
  ) {}

  public requestPasswordUpdate(
    currentPassword: string,
    newPassword: string,
  ): Observable<User> {
    //const apiUri = `${BASE_API_URI}/app/users/password`;
    const apiUri = `${this._configService.baseApiUri}/app/users/password`;
    const payload = this.buildPayload(currentPassword, newPassword);
    return this._httpClient.put<User>(apiUri, payload);
  }

  private buildPayload(currentPassword: string, newPassword: string): object {
    return {
      command: {
        token: this._authService.userData.token,
        currentPassword: currentPassword,
        newPassword: newPassword,
      },
    };
  }
}
