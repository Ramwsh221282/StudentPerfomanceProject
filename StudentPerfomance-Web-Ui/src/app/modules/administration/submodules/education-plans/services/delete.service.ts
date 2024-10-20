import { inject, Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { EducationPlan } from '../models/education-plan-interface';
import { Observable } from 'rxjs';
import { User } from '../../../../users/services/user-interface';
import { AuthService } from '../../../../users/services/auth.service';

@Injectable({
  providedIn: 'any',
})
export class DeleteService extends BaseService {
  private readonly _user: User;

  public constructor() {
    super();
    const authService = inject(AuthService);
    this._user = { ...authService.userData };
  }

  public delete(plan: EducationPlan): Observable<EducationPlan> {
    const body = this.buildPayload(plan);
    return this.httpClient.delete<EducationPlan>(
      `${this.managementApiUri}remove`,
      {
        body,
      }
    );
  }

  private buildPayload(plan: EducationPlan): object {
    return {
      plan: plan,
      token: this._user.token,
    };
  }
}
