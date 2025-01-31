import { Injectable } from '@angular/core';
import { BaseHttpService } from '../../../shared/models/common/base-http/base-http.service';
import { UserRecord } from '../../../modules/administration/submodules/users/services/user-table-element-interface';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'any' })
export class RemoveUserService extends BaseHttpService {
  public remove(user: UserRecord): Observable<UserRecord> {
    const url = `${this._config.baseApiUri}/api/users`;
    const headers = this.buildHttpHeaders();
    const payload = this.buildPayload(user);
    return this._http.delete<UserRecord>(url, {
      headers: headers,
      body: payload,
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
        token: this._auth.userData.token,
      },
    };
  }
}
