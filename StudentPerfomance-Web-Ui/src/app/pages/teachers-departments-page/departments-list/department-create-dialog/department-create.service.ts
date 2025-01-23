import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthService } from '../../../user-page/services/auth.service';
import { AppConfigService } from '../../../../app.config.service';
import { Department } from '../../../../modules/administration/submodules/departments/models/departments.interface';
import { DepartmentPayloadBuilder } from '../../../../modules/administration/submodules/departments/models/contracts/department-contract/department-payload-builder';
import { TokenPayloadBuilder } from '../../../../shared/models/common/token-contract/token-payload-builder';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'any',
})
export class DepartmentCreateService {
  public constructor(
    private readonly _http: HttpClient,
    private readonly _auth: AuthService,
    private readonly _config: AppConfigService,
  ) {}

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

  private buildHttpHeaders(): HttpHeaders {
    return new HttpHeaders().set('token', this._auth.userData.token);
  }
}
