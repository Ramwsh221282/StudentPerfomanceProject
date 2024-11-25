import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BASE_API_URI } from '../../../models/api/api-constants';
import { AuthService } from '../../../../modules/users/services/auth.service';
import { User } from '../../../../modules/users/services/user-interface';

@Injectable({
  providedIn: 'any',
})
export class UserEmailUpdateService {
  public constructor(
    private readonly _httpClient: HttpClient,
    private readonly _authService: AuthService,
  ) {}

  public requestEmailUpdate(
    password: string,
    newEmail: string,
  ): Observable<User> {
    const apiUri = `${BASE_API_URI}/app/users/email`;
    const payload = this.buildRequestBody(password, newEmail);
    return this._httpClient.put<User>(apiUri, payload);
  }

  private buildRequestBody(password: string, newEmail: string): object {
    return {
      command: {
        token: this._authService.userData.token,
        currentEmail: this._authService.userData.email,
        newEmail: newEmail,
        password: password,
      },
    };
  }
}
