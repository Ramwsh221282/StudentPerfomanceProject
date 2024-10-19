import { HttpClient } from '@angular/common/http';
import { inject } from '@angular/core';
import { BASE_API_URI } from '../../../../../shared/models/api/api-constants';
import { User } from '../../../../users/services/user-interface';
import { AuthService } from '../../../../users/services/auth.service';
import { Observable } from 'rxjs';
import { UserRecord } from './user-table-element-interface';

export class UserCreationService {
  private readonly _httpClient: HttpClient;
  private readonly _apiUri: string;
  private readonly _user: User;

  public constructor() {
    this._httpClient = inject(HttpClient);
    this._apiUri = `${BASE_API_URI}/api/users/management/create`;
    const authService = inject(AuthService);
    this._user = { ...authService.userData };
  }

  public create(user: User): Observable<UserRecord> {
    const payload = this.buildPayload(user);
    return this._httpClient.post<UserRecord>(this._apiUri, payload);
  }

  private buildPayload(user: User): object {
    return {
      user: {
        email: user.email,
        name: user.name,
        surname: user.surname,
        thirdname: user.thirdname,
        role: user.role,
      },
      token: this._user.token,
    };
  }
}
