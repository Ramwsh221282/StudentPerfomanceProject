import { Injectable } from '@angular/core';
import { StudentGroupsService } from './student-groups-base-service';
import { StudentGroup } from './studentsGroup.interface';
import { Observable } from 'rxjs';
import { AuthService } from '../../../../users/services/auth.service';
import { TokenPayloadBuilder } from '../../../../../shared/models/common/token-contract/token-payload-builder';
import { BASE_API_URI } from '../../../../../shared/models/api/api-constants';
import { StudentGroupPayloadBuilder } from '../models/contracts/student-group-contract/student-group-payload-builder';

@Injectable({
  providedIn: 'any',
})
export class StudentGroupSearchService extends StudentGroupsService {
  public constructor(private readonly _authService: AuthService) {
    super();
  }

  public getAllGroups(): Observable<StudentGroup[]> {
    const payload = {
      token: TokenPayloadBuilder(this._authService.userData),
    };
    return this.httpClient.post<StudentGroup[]>(
      `${BASE_API_URI}/api/student-groups/all`,
      payload
    );
  }

  public searchGroups(group: StudentGroup): Observable<StudentGroup[]> {
    const payload = {
      group: StudentGroupPayloadBuilder(group),
      token: TokenPayloadBuilder(this._authService.userData),
    };
    return this.httpClient.post<StudentGroup[]>(
      `${BASE_API_URI}/api/student-groups/search`,
      payload
    );
  }
}
