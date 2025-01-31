import { Injectable } from '@angular/core';
import { Department } from '../../../../modules/administration/submodules/departments/models/departments.interface';
import { DepartmentPayloadBuilder } from '../../../../modules/administration/submodules/departments/models/contracts/department-contract/department-payload-builder';
import { TokenPayloadBuilder } from '../../../../shared/models/common/token-contract/token-payload-builder';
import { Observable } from 'rxjs';
import { BaseHttpService } from '../../../../shared/models/common/base-http/base-http.service';

@Injectable({
  providedIn: 'any',
})
export class DepartmentCreateService extends BaseHttpService {
  public create(department: Department): Observable<Department> {
    const url = `${this._config.baseApiUri}/api/teacher-departments`;
    const headers = this.buildHttpHeaders();
    const payload = this.buildPayload(department);
    return this._http.post<Department>(url, payload, { headers: headers });
  }

  private buildPayload(department: Department): object {
    return {
      department: DepartmentPayloadBuilder(department),
      token: TokenPayloadBuilder(this._auth.userData),
    };
  }
}
