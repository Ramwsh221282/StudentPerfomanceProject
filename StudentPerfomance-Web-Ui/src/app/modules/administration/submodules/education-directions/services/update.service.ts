import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { EducationDirection } from '../models/education-direction-interface';
import { Observable } from 'rxjs';
import { AuthService } from '../../../../users/services/auth.service';
import { BASE_API_URI } from '../../../../../shared/models/api/api-constants';
import { DirectionPayloadBuilder } from '../models/contracts/direction-payload-builder';
import { HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'any',
})
export class UpdateService extends BaseService {
  public constructor(private readonly _authService: AuthService) {
    super();
  }

  public update(
    initial: EducationDirection,
    updated: EducationDirection,
  ): Observable<EducationDirection> {
    const headers = this.buildHttpHeaders();
    const body = this.buildPayload(initial, updated);
    return this.httpClient.put<EducationDirection>(
      `${BASE_API_URI}/api/education-direction`,
      body,
      {
        headers: headers,
      },
    );
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
