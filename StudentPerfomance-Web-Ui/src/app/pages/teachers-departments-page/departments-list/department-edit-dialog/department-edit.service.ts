import { Injectable } from '@angular/core';
import { Department } from '../../../../modules/administration/submodules/departments/models/departments.interface';
import { DepartmentPayloadBuilder } from '../../../../modules/administration/submodules/departments/models/contracts/department-contract/department-payload-builder';
import { TokenPayloadBuilder } from '../../../../shared/models/common/token-contract/token-payload-builder';
import { Observable } from 'rxjs';
import { BaseHttpService } from '../../../../shared/models/common/base-http/base-http.service';

@Injectable({
  providedIn: 'any',
})
export class DepartmentEditService extends BaseHttpService {
  public edit(
    initial: Department,
    updated: Department,
  ): Observable<Department> {
    const url = `${this._config.baseApiUri}/api/teacher-departments`;
    const headers = this.buildHttpHeaders();
    const payload = this.buildPayload(initial, updated);
    return this._http.put<Department>(url, payload, { headers: headers });
  }

  private buildPayload(initial: Department, updated: Department): object {
    return {
      initial: DepartmentPayloadBuilder(initial),
      updated: DepartmentPayloadBuilder(updated),
      token: TokenPayloadBuilder(this._auth.userData),
    };
  }
}
