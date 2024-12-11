import { Injectable } from '@angular/core';
import { EducationDirection } from '../models/education-direction-interface';
import { BaseService } from './base.service';
import { Observable } from 'rxjs';
import { AuthService } from '../../../../users/services/auth.service';
//import { BASE_API_URI } from '../../../../../shared/models/api/api-constants';
import { DirectionPayloadBuilder } from '../models/contracts/direction-payload-builder';
import { HttpHeaders } from '@angular/common/http';
import { AppConfigService } from '../../../../../app.config.service';

@Injectable({
  providedIn: 'any',
})
export class CreateService extends BaseService {
  constructor(
    private readonly _authService: AuthService,
    private readonly _appConfig: AppConfigService,
  ) {
    super();
  }

  public create(direction: EducationDirection): Observable<EducationDirection> {
    //const apiUri = `${BASE_API_URI}/api/education-direction`;
    const apiUri = `${this._appConfig.baseApiUri}/api/education-direction`;
    const headers = this.buildHttpHeaders();
    const body = this.buildPayload(direction);
    return this.httpClient.post<EducationDirection>(apiUri, body, {
      headers: headers,
    });
  }

  private buildHttpHeaders(): HttpHeaders {
    const headers = new HttpHeaders().set(
      'token',
      this._authService.userData.token,
    );
    return headers;
  }

  private buildPayload(direction: EducationDirection): object {
    return {
      command: DirectionPayloadBuilder(direction),
    };
  }
}
