import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { EducationDirection } from '../models/education-direction-interface';
import { Observable } from 'rxjs';
import { AuthService } from '../../../../users/services/auth.service';
//import { BASE_API_URI } from '../../../../../shared/models/api/api-constants';
import { DirectionPayloadBuilder } from '../models/contracts/direction-payload-builder';
import { HttpHeaders } from '@angular/common/http';
import { AppConfigService } from '../../../../../app.config.service';

@Injectable({
  providedIn: 'any',
})
export class UpdateService extends BaseService {
  public constructor(
    private readonly _authService: AuthService,
    private readonly _appConfig: AppConfigService,
  ) {
    super();
  }

  public update(
    initial: EducationDirection,
    updated: EducationDirection,
  ): Observable<EducationDirection> {
    //const apiUri = `${BASE_API_URI}/api/education-direction`;
    const apiUri = `${this._appConfig.baseApiUri}/api/education-direction`;
    const headers = this.buildHttpHeaders();
    const body = this.buildPayload(initial, updated);
    return this.httpClient.put<EducationDirection>(apiUri, body, {
      headers: headers,
    });
  }

  private buildHttpHeaders(): HttpHeaders {
    return new HttpHeaders().set('token', this._authService.userData.token);
  }

  private buildPayload(
    initial: EducationDirection,
    updated: EducationDirection,
  ): object {
    return {
      initial: DirectionPayloadBuilder(initial),
      updated: DirectionPayloadBuilder(updated),
    };
  }
}
