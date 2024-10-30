import { Injectable } from '@angular/core';
import { StudentGroupsService } from './student-groups-base-service';
import { StudentGroup } from './studentsGroup.interface';
import { BASE_API_URI } from '../../../../../shared/models/api/api-constants';
import { StudentGroupPayloadBuilder } from '../models/contracts/student-group-contract/student-group-payload-builder';
import { TokenPayloadBuilder } from '../../../../../shared/models/common/token-contract/token-payload-builder';
import { AuthService } from '../../../../users/services/auth.service';

@Injectable({
  providedIn: 'any',
})
export class StudentGroupsUpdateDataService extends StudentGroupsService {
  private readonly _apiUri: string = `${BASE_API_URI}/api/student-groups`;
  public constructor(private readonly _authService: AuthService) {
    super();
  }

  public update(initial: StudentGroup, updated: StudentGroup) {
    const payload = this.buildPayload(initial, updated);
    return this.httpClient.put<StudentGroup>(this._apiUri, payload);
  }

  private buildPayload(initial: StudentGroup, updated: StudentGroup): object {
    return {
      initial: StudentGroupPayloadBuilder(initial),
      updated: StudentGroupPayloadBuilder(updated),
      token: TokenPayloadBuilder(this._authService.userData),
    };
  }
}
