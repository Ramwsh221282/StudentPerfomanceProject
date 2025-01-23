import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthService } from '../../../user-page/services/auth.service';
import { AppConfigService } from '../../../../app.config.service';
import { Teacher } from '../../../../modules/administration/submodules/teachers/models/teacher.interface';
import { Department } from '../../../../modules/administration/submodules/departments/models/departments.interface';
import { DepartmentPayloadBuilder } from '../../../../modules/administration/submodules/departments/models/contracts/department-contract/department-payload-builder';
import { TeacherPayloadBuilder } from '../../../../modules/administration/submodules/teachers/contracts/teacher-contract/teacher-payload-builder';
import { TokenPayloadBuilder } from '../../../../shared/models/common/token-contract/token-payload-builder';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'any',
})
export class RemoveTeacherService {
  public constructor(
    private readonly _http: HttpClient,
    private readonly _auth: AuthService,
    private readonly _config: AppConfigService,
  ) {}

  public remove(teacher: Teacher, department: Department): Observable<Teacher> {
    const url = `${this._config.baseApiUri}/api/teachers`;
    const headers = this.buildHttpHeaders();
    const payload = this.buildPayload(teacher, department);
    return this._http.delete<Teacher>(url, { headers: headers, body: payload });
  }

  private buildHttpHeaders(): HttpHeaders {
    return new HttpHeaders().set('token', this._auth.userData.token);
  }

  private buildPayload(teacher: Teacher, department: Department): object {
    return {
      department: DepartmentPayloadBuilder(department),
      teacher: TeacherPayloadBuilder(teacher),
      token: TokenPayloadBuilder(this._auth.userData),
    };
  }
}
