import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthService } from '../../user-page/services/auth.service';
import { AppConfigService } from '../../../app.config.service';
import { Observable } from 'rxjs';
import { StudentGroup } from '../../../modules/administration/submodules/student-groups/services/studentsGroup.interface';

@Injectable({
  providedIn: 'any',
})
export class StudentGroupsDataService {
  public constructor(
    private readonly _http: HttpClient,
    private readonly _auth: AuthService,
    private readonly _config: AppConfigService,
  ) {}

  public getGroups(): Observable<StudentGroup[]> {
    const url = `${this._config.baseApiUri}/api/student-groups/all`;
    const headers = this.buildHttpHeaders();
    return this._http.get<StudentGroup[]>(url, { headers: headers });
  }

  private buildHttpHeaders(): HttpHeaders {
    return new HttpHeaders().set('token', this._auth.userData.token);
  }
}
