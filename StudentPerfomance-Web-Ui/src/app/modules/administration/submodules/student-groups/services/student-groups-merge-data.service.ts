import { inject, Injectable } from '@angular/core';
import { StudentGroupsService } from './student-groups-base-service';
import { StudentGroup } from './studentsGroup.interface';
//import { BASE_API_URI } from '../../../../../shared/models/api/api-constants';
import { AuthService } from '../../../../users/services/auth.service';
import { StudentGroupPayloadBuilder } from '../models/contracts/student-group-contract/student-group-payload-builder';
import { TokenPayloadBuilder } from '../../../../../shared/models/common/token-contract/token-payload-builder';
import { HttpHeaders } from '@angular/common/http';
import { AppConfigService } from '../../../../../app.config.service';

@Injectable({
  providedIn: 'any',
})
export class StudentGroupsMergeDataService extends StudentGroupsService {
  private readonly _apiUri: string;

  public constructor(private readonly _authService: AuthService) {
    super();
    //this._apiUri = `${BASE_API_URI}/api/student-groups/merge`;
    const appConfig = inject(AppConfigService);
    this._apiUri = `${appConfig.baseApiUri}/api/student-groups/merge`;
  }

  public merge(initial: StudentGroup, target: StudentGroup) {
    const payload = this.buildPayload(initial, target);
    const headers = this.buildHttpHeaders();
    return this.httpClient.put<StudentGroup>(this._apiUri, payload, {
      headers: headers,
    });
  }

  private buildPayload(initial: StudentGroup, target: StudentGroup): object {
    return {
      initial: StudentGroupPayloadBuilder(initial),
      target: StudentGroupPayloadBuilder(target),
      token: TokenPayloadBuilder(this._authService.userData),
    };
  }

  private buildHttpHeaders(): HttpHeaders {
    return new HttpHeaders().set('token', this._authService.userData.token);
  }
}
