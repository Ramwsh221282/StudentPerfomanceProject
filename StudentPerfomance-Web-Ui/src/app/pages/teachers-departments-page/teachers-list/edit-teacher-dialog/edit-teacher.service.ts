import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthService } from '../../../user-page/services/auth.service';
import { AppConfigService } from '../../../../app.config.service';
import { Observable } from 'rxjs';
import { Teacher } from '../../../../modules/administration/submodules/teachers/models/teacher.interface';
import { DepartmentPayloadBuilder } from '../../../../modules/administration/submodules/departments/models/contracts/department-contract/department-payload-builder';
import { TeacherPayloadBuilder } from '../../../../modules/administration/submodules/teachers/contracts/teacher-contract/teacher-payload-builder';
import { TokenPayloadBuilder } from '../../../../shared/models/common/token-contract/token-payload-builder';
import { Department } from '../../../../modules/administration/submodules/departments/models/departments.interface';

@Injectable({
  providedIn: 'any',
})
export class EditTeacherService {
  public constructor(
    private readonly _http: HttpClient,
    private readonly _auth: AuthService,
    private readonly _config: AppConfigService,
  ) {}

  public edit(
    initial: Teacher,
    updated: Teacher,
    department: Department,
  ): Observable<Teacher> {
    const url = `${this._config.baseApiUri}/api/teachers`;
    const headers = this.buildHttpHeaders();
    const payload = this.buildPayload(initial, updated, department);
    return this._http.put<Teacher>(url, payload, { headers: headers });
  }

  private buildHttpHeaders(): HttpHeaders {
    return new HttpHeaders().set('token', this._auth.userData.token);
  }

  private buildPayload(
    initial: Teacher,
    updated: Teacher,
    department: Department,
  ): object {
    return {
      department: DepartmentPayloadBuilder(department),
      initial: TeacherPayloadBuilder(initial),
      updated: TeacherPayloadBuilder(updated),
      token: TokenPayloadBuilder(this._auth.userData),
    };
  }
}
