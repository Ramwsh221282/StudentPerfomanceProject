import { BaseHttpService } from '../../../shared/models/common/base-http/base-http.service';
import { Injectable } from '@angular/core';
import { UserRecord } from '../../../modules/administration/submodules/users/services/user-table-element-interface';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'any',
})
export class CreateUserService extends BaseHttpService {
  public create(user: UserRecord): Observable<UserRecord> {
    const url = `${this._config.baseApiUri}/api/users`;
    const headers = this.buildHttpHeaders();
    const payload = this.buildPayload(user);
    return this._http.post<UserRecord>(url, payload, { headers: headers });
  }

  private buildPayload(user: UserRecord): object {
    return {
      user: {
        email: user.email,
        name: user.name,
        surname: user.surname,
        patronymic: user.patronymic,
        role: user.role,
      },
      token: {
        token: this._auth.userData.token,
      },
    };
  }
}
