import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthService } from '../../../../../user-page/services/auth.service';
import { AppConfigService } from '../../../../../../app.config.service';
import { Observable } from 'rxjs';
import { Department } from '../../../../../../modules/administration/submodules/departments/models/departments.interface';

@Injectable({
  providedIn: 'any',
})
export class TeachersDepartmentsDataService {
  constructor(
    private readonly _http: HttpClient,
    private readonly _auth: AuthService,
    private readonly _config: AppConfigService,
  ) {}

  public getDepartments(): Observable<Department[]> {
    const url = `${this._config.baseApiUri}/api/teacher-departments/all`;
    const headers = this.buildHttpHeaders();
    return this._http.get<Department[]>(url, { headers: headers });
  }

  private buildHttpHeaders(): HttpHeaders {
    return new HttpHeaders().set('token', this._auth.userData.token);
  }
}
