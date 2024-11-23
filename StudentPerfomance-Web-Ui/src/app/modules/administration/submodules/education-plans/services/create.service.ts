import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { EducationPlan } from '../models/education-plan-interface';
import { Observable } from 'rxjs';
import { AuthService } from '../../../../users/services/auth.service';
import { BASE_API_URI } from '../../../../../shared/models/api/api-constants';
import { DirectionPayloadBuilder } from '../../education-directions/models/contracts/direction-payload-builder';
import { EducationPlanPayloadBuilder } from '../models/contracts/education-plan-contract/education-plan-payload-builder';
import { HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'any',
})
export class CreateService extends BaseService {
  public constructor(private readonly _authService: AuthService) {
    super();
  }

  public create(plan: EducationPlan): Observable<EducationPlan> {
    const headers = this.buildHttpHeaders();
    const body = this.buildPayload(plan);
    return this.httpClient.post<EducationPlan>(
      `${BASE_API_URI}/api/education-plans`,
      body,
      {
        headers: headers,
      },
    );
  }

  private buildHttpHeaders(): HttpHeaders {
    return new HttpHeaders().set('token', this._authService.userData.token);
  }

  private buildPayload(plan: EducationPlan): object {
    return {
      direction: DirectionPayloadBuilder(plan.direction),
      plan: EducationPlanPayloadBuilder(plan),
    };
  }
}
