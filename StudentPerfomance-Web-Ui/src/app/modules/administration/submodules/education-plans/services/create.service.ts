import { inject, Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { EducationPlan } from '../models/education-plan-interface';
import { Observable } from 'rxjs';
import { User } from '../../../../users/services/user-interface';
import { AuthService } from '../../../../users/services/auth.service';
import { BASE_API_URI } from '../../../../../shared/models/api/api-constants';
import { DirectionPayloadBuilder } from '../../education-directions/models/contracts/direction-payload-builder';
import { EducationPlanPayloadBuilder } from '../models/contracts/education-plan-contract/education-plan-payload-builder';
import { TokenPayloadBuilder } from '../../../../../shared/models/common/token-contract/token-payload-builder';

@Injectable({
  providedIn: 'any',
})
export class CreateService extends BaseService {
  private readonly _user: User;

  public constructor() {
    super();
    const authService = inject(AuthService);
    this._user = { ...authService.userData };
  }

  public create(plan: EducationPlan): Observable<EducationPlan> {
    const body = this.buildPayload(plan);
    return this.httpClient.post<EducationPlan>(
      `${BASE_API_URI}/api/education-plans`,
      body
    );
  }

  private buildPayload(plan: EducationPlan): object {
    return {
      direction: DirectionPayloadBuilder(plan.direction),
      plan: EducationPlanPayloadBuilder(plan),
      token: TokenPayloadBuilder(this._user),
    };
  }
}
