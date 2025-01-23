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
export class DepartmentEditService {
  public constructor(
    private readonly _http: HttpClient,
    private readonly _auth: AuthService,
    private readonly _config: AppConfigService,
  ) {}

  public edit(
    initial: Department,
    updated: Department,
  ): Observable<Department> {
    const url = `${this._config.baseApiUri}/api/teacher-departments`;
    const headers = this.buildHttpHeaders();
    const payload = this.buildPayload(initial, updated);
    return this._http.put<Department>(url, payload, { headers: headers });
  }

  private buildHttpHeaders(): HttpHeaders {
    return new HttpHeaders().set('token', this._auth.userData.token);
  }

  private buildPayload(initial: Department, updated: Department): object {
    return {
      initial: DepartmentPayloadBuilder(initial),
      updated: DepartmentPayloadBuilder(updated),
      token: TokenPayloadBuilder(this._auth.userData),
    };
  }
}
