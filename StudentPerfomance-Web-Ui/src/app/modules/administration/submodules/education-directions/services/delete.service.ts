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
export class DeleteService extends BaseService {
  constructor(private readonly _authService: AuthService) {
    super();
  }

  public delete(direction: EducationDirection): Observable<EducationDirection> {
    const body = this.buildPayload(direction);
    const headers = this.buildHttpHeaders();
    return this.httpClient.delete<EducationDirection>(
      `${BASE_API_URI}/api/education-direction`,
      {
        headers: headers,
        body,
      },
    );
  }

  private buildHttpHeaders(): HttpHeaders {
    return new HttpHeaders().set('token', this._authService.userData.token);
  }

  private buildPayload(direction: EducationDirection): object {
    return {
      query: DirectionPayloadBuilder(direction),
    };
  }
}
