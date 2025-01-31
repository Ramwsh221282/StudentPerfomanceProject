import { Injectable } from '@angular/core';
import { BaseHttpService } from '../../shared/models/common/base-http/base-http.service';
import { Observable } from 'rxjs';
import { UserRecord } from '../../modules/administration/submodules/users/services/user-table-element-interface';

@Injectable({ providedIn: 'any' })
export class UsersDataService extends BaseHttpService {
  public getUsers(): Observable<UserRecord[]> {
    const url = `${this._config.baseApiUri}/api/users/all`;
    const headers = this.buildHttpHeaders();
    return this._http.get<UserRecord[]>(url, { headers: headers });
  }
}
