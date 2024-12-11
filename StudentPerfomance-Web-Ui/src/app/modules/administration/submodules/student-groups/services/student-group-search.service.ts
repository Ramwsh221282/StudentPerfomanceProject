import { inject, Injectable } from '@angular/core';
import { StudentGroupsService } from './student-groups-base-service';
import { StudentGroup } from './studentsGroup.interface';
import { Observable } from 'rxjs';
import { AuthService } from '../../../../users/services/auth.service';
//import { BASE_API_URI } from '../../../../../shared/models/api/api-constants';
import { HttpHeaders, HttpParams } from '@angular/common/http';
import { AppConfigService } from '../../../../../app.config.service';

@Injectable({
  providedIn: 'any',
})
export class StudentGroupSearchService extends StudentGroupsService {
  private readonly _appConfig: AppConfigService;

  public constructor(private readonly _authService: AuthService) {
    super();
    this._appConfig = inject(AppConfigService);
  }

  public getAllGroups(): Observable<StudentGroup[]> {
    //const apiUri = `${BASE_API_URI}/api/student-groups/all`;
    const apiUri = `${this._appConfig.baseApiUri}/api/student-groups/all`;
    const headers = this.buildHttpHeaders();
    return this.httpClient.get<StudentGroup[]>(apiUri, {
      headers: headers,
    });
  }

  public searchGroups(group: StudentGroup): Observable<StudentGroup[]> {
    //const apiUri = `${BASE_API_URI}/api/student-groups/search`;
    const apiUri = `${this._appConfig.baseApiUri}/api/student-groups/search`;
    const params = this.buildHttpParams(group);
    const headers = this.buildHttpHeaders();
    return this.httpClient.post<StudentGroup[]>(apiUri, {
      headers: headers,
      params,
    });
  }

  private buildHttpParams(group: StudentGroup): HttpParams {
    return new HttpParams().set('groupName', group.name);
  }

  private buildHttpHeaders(): HttpHeaders {
    return new HttpHeaders().set('token', this._authService.userData.token);
  }
}
