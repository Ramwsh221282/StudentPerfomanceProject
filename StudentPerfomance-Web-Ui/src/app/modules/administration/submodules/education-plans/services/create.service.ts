import { inject, Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { EducationPlan } from '../models/education-plan-interface';
import { Observable } from 'rxjs';
import { User } from '../../../../users/services/user-interface';
import { AuthService } from '../../../../users/services/auth.service';

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
      `${this.managementApiUri}create`,
      body
    );
  }

  private buildPayload(plan: EducationPlan): object {
    return {
      plan: plan,
      token: this._user.token,
    };
  }
}
