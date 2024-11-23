import { Injectable } from '@angular/core';
import { StudentGroupsService } from './student-groups-base-service';
import { StudentGroup } from './studentsGroup.interface';
import { BASE_API_URI } from '../../../../../shared/models/api/api-constants';
import { AuthService } from '../../../../users/services/auth.service';
import { TokenPayloadBuilder } from '../../../../../shared/models/common/token-contract/token-payload-builder';
import { HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'any',
})
export class StudentGroupsCreateDataService extends StudentGroupsService {
  private readonly _apiUri: string = `${BASE_API_URI}/api/student-groups`;

  public constructor(private readonly _authService: AuthService) {
    super();
  }

  public create(group: StudentGroup) {
    const payload = this.buildPayload(group);
    const headers = this.buildHttpHeaders();
    return this.httpClient.post<StudentGroup>(this._apiUri, payload, {
      headers: headers,
    });
  }

  private buildPayload(group: StudentGroup): object {
    return {
      group: {
        name: group.name,
      },
      token: TokenPayloadBuilder(this._authService.userData),
    };
  }

  private buildHttpHeaders(): HttpHeaders {
    return new HttpHeaders().set('token', this._authService.userData.token);
  }
}
