import { Injectable } from '@angular/core';
import { StudentGroupsService } from './student-groups-base-service';
import { StudentGroup } from './studentsGroup.interface';
import { Observable } from 'rxjs';
import { AuthService } from '../../../../users/services/auth.service';
import { BASE_API_URI } from '../../../../../shared/models/api/api-constants';
import { HttpHeaders, HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'any',
})
export class StudentGroupSearchService extends StudentGroupsService {
  public constructor(private readonly _authService: AuthService) {
    super();
  }

  public getAllGroups(): Observable<StudentGroup[]> {
    const headers = this.buildHttpHeaders();
    return this.httpClient.get<StudentGroup[]>(
      `${BASE_API_URI}/api/student-groups/all`,
      {
        headers: headers,
      },
    );
  }

  public searchGroups(group: StudentGroup): Observable<StudentGroup[]> {
    const params = this.buildHttpParams(group);
    const headers = this.buildHttpHeaders();
    return this.httpClient.post<StudentGroup[]>(
      `${BASE_API_URI}/api/student-groups/search`,
      {
        headers: headers,
        params,
      },
    );
  }

  private buildHttpParams(group: StudentGroup): HttpParams {
    return new HttpParams().set('groupName', group.name);
  }

  private buildHttpHeaders(): HttpHeaders {
    return new HttpHeaders().set('token', this._authService.userData.token);
  }
}
