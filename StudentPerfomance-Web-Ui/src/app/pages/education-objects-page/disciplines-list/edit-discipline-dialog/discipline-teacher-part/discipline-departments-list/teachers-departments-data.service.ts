import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Department } from '../../../../../../modules/administration/submodules/departments/models/departments.interface';
import { BaseHttpService } from '../../../../../../shared/models/common/base-http/base-http.service';

@Injectable({
  providedIn: 'any',
})
export class TeachersDepartmentsDataService extends BaseHttpService {
  public getDepartments(): Observable<Department[]> {
    const url = `${this._config.baseApiUri}/api/teacher-departments/all`;
    const headers = this.buildHttpHeaders();
    return this._http.get<Department[]>(url, { headers: headers });
  }
}
