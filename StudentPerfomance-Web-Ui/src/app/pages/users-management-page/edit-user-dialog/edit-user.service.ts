import { Injectable } from '@angular/core';
import { BaseHttpService } from '../../../shared/models/common/base-http/base-http.service';
import { UserRecord } from '../../../modules/administration/submodules/users/services/user-table-element-interface';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'any',
})
export class EditUserService extends BaseHttpService {
  public updateUser(
    initial: UserRecord,
    updated: UserRecord,
  ): Observable<UserRecord> {
    const headers = this.buildHttpHeaders();
    const payload = this.buildPayload(initial, updated);
    const url = `${this._config.baseApiUri}/api/users`;
    return this._http.put<UserRecord>(url, payload, { headers: headers });
  }

  private buildPayload(initial: UserRecord, updated: UserRecord): object {
    return {
      id: initial.id,
      name: updated.name,
      surname: updated.surname,
      patronymic: updated.patronymic,
    };
  }
}
