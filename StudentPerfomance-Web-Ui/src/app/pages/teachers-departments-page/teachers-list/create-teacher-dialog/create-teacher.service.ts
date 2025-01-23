import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthService } from '../../../user-page/services/auth.service';
import { AppConfigService } from '../../../../app.config.service';
import { Injectable } from '@angular/core';
import { Teacher } from '../../../../modules/administration/submodules/teachers/models/teacher.interface';
import { Department } from '../../../../modules/administration/submodules/departments/models/departments.interface';
import { Observable } from 'rxjs';
import { DepartmentPayloadBuilder } from '../../../../modules/administration/submodules/departments/models/contracts/department-contract/department-payload-builder';
import { TeacherPayloadBuilder } from '../../../../modules/administration/submodules/teachers/contracts/teacher-contract/teacher-payload-builder';

@Injectable({
  providedIn: 'any',
})
export class CreateTeacherService {
  constructor(
    private readonly _http: HttpClient,
    private readonly _auth: AuthService,
    private readonly _config: AppConfigService,
  ) {}

  public create(teacher: Teacher, department: Department): Observable<Teacher> {
    const url = `${this._config.baseApiUri}/api/teachers`;
    const headers = this.buildHttpHeaders();
    const payload = this.buildPayload(teacher, department);
    return this._http.post<Teacher>(url, payload, { headers: headers });
  }

  private buildHttpHeaders(): HttpHeaders {
    return new HttpHeaders().set('token', this._auth.userData.token);
  }

  private buildPayload(teacher: Teacher, department: Department): object {
    return {
      department: DepartmentPayloadBuilder(department),
      teacher: TeacherPayloadBuilder(teacher),
    };
  }
}
