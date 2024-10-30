import { Injectable } from '@angular/core';
import { StudentGroupsService } from './student-groups-base-service';
import { StudentGroup } from './studentsGroup.interface';
import { BASE_API_URI } from '../../../../../shared/models/api/api-constants';
import { AuthService } from '../../../../users/services/auth.service';
import { StudentGroupPayloadBuilder } from '../models/contracts/student-group-contract/student-group-payload-builder';
import { TokenPayloadBuilder } from '../../../../../shared/models/common/token-contract/token-payload-builder';

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
    return this.httpClient.post<StudentGroup>(this._apiUri, payload);
  }

  private buildPayload(group: StudentGroup): object {
    return {
      group: {
        name: group.name,
      },
      token: TokenPayloadBuilder(this._authService.userData),
    };
  }
}
